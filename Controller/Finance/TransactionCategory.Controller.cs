using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Route("v1/finance/transaction-categories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionCategoryController: AppControllerBase
    {
        private readonly LookupRepository _lookupRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public TransactionCategoryController(AppDbContext context, ILogger<TransactionCategoryController> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TransactionCategory>> GetTransactionCategories()
        {
            return Ok(_context.TransactionCategory.OrderBy(c => c.Name));
        }

        [Authorize]
        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<TransactionCategory>> GetTransactionCategoriesPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name
        ) 
        {
            var items = _lookupRepository.OrderBy<TransactionCategory>(_lookupRepository.Filter<TransactionCategory>(_context.TransactionCategory, name), sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TransactionCategory> GetTransactionCategory(int id)
        {
            return Ok(_context.TransactionCategory.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<TransactionCategory> CreateTransactionCategory([FromBody] TransactionCategory item)
        {
            return Create<TransactionCategory>(item, nameof(CreateTransactionCategory));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateTransactionCategory(int id, [FromBody] TransactionCategory item)
        {
            return Update<TransactionCategory>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteTransactionCategory (int id)
        {
            return Delete<TransactionCategory>(id);
        }



    }

}