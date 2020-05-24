using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using B.API.Database;

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


        [HttpGet("summary/{year}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<FinancialSummary> GetSummary(string year) 
        {
            return Ok(_repository.GetSummary(year));
        }

        [HttpGet("spending-categories")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionCategoryTotal>> GetTransactionCategoryTotals(
            [FromQuery] string year
        ) {
            return Ok(_repository.FindTransactionCategoryTotals(year));
        }

        [HttpGet("expenses")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Expense>> GetExpenses(
            [FromQuery] string year,
            [FromQuery] string month
        ) {
           return Ok(_repository.FindMonthlyExpenses(year, month));
        }

        [HttpGet("expense-summary")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<ExpenseSummary> GetExpenseSummary(
            [FromQuery] string year,
            [FromQuery] string month
        ) {
           return Ok(new ExpenseSummary { expenses = _repository.FindMonthlyExpenses(year, month)});
        }

   }
}