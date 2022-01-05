using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using B.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Authorize]
    [Route("v1/finance")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FinanceController: AppControllerBase
    {
        private readonly AppDbContext _context;


        private readonly FinanceRepository _repository;


        private readonly ILogger _logger;
        public FinanceController(AppDbContext context, ILogger<FinanceController> logger, FinanceRepository repository): base(context, logger)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
        }


        [HttpGet("categories")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionCategory>> GetCategories(string year) 
        {
            return Ok(_context.TransactionCategories.AsNoTracking().OrderBy(o => o.Name));
        }


        [HttpGet("summary/{year}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<FinancialSummary> GetSummary(string year) 
        {
            return Ok(_repository.GetSummary(year));
        }


        [HttpGet("monthly-totals")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionTotal>> GetTransactionMonthlyTotals(
            [FromQuery] string year
        ) {
            return Ok(_repository.FindTransactionMonthlyTotals(year));
        }

        [HttpGet("spending-categories")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionTotal>> GetTransactionCategoryTotals(
            [FromQuery] string year
        ) {
            return Ok(_repository.FindTransactionCategoryTotals(year));
        }

        [HttpGet("spending-categories-by-month")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionTotal>> GetTransactionCategoryMonthlyTotals(
            [FromQuery] string year,
            [FromQuery] string month 
        ) {
            return Ok(_repository.FindTransactionCategoryMonthlyTotals(year, month));
        }


        [HttpGet("spending-category-tags")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionTotal>> GetTransactionCategoryTagTotals(
            [FromQuery] string year,
            [FromQuery] string categoryName
        ) {
            return Ok(_repository.FindTransactionCategoryTagTotals(year, categoryName));
        }

        [HttpGet("frequent-category-tags")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<RecordCount>> GetFrequentCategoryTags(
            [FromQuery] int categoryId
        ) {
            return Ok(_repository.FindMostUsedCategoryTags(categoryId));
        }

        [HttpGet("expenses")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<ExpenseSummary>> GetExpenses(
            [FromQuery] List<string> years,
            [FromQuery] List<string> months,
            [FromQuery] List<long> categories
        ) {
            if (years.Count > 0 && months.Count == 0) {
                return Ok(new ExpenseSummary { expenses = _repository.FindYearlyExpenses(years, categories)});
            }
            else if (years.Count > 0 && months.Count > 0) {
                return Ok(new ExpenseSummary { expenses =_repository.FindMonthlyExpenses(years, months, categories)});
            }
            return BadRequest();
        }

        [HttpGet("expense-summary")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<ExpenseSummary> GetExpenseSummary(
            [FromQuery] List<string> years,
            [FromQuery] List<string> months,
            [FromQuery] List<long> categories 
        ) {
            if (years.Count > 0 && months.Count == 0) {
                return Ok(new ExpenseSummary { expenses = _repository.FindYearlyExpenses(years, categories)});
            }
            else if (years.Count > 0 && months.Count > 0) {
                return Ok(new ExpenseSummary { expenses = _repository.FindMonthlyExpenses(years, months, categories)});
            }
            return BadRequest();
        }

   }
}