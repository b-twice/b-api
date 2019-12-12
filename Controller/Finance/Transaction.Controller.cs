using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Authorize]
    [Route("v1/finance/transactions")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionController: AppControllerBase
    {
        private readonly TransactionRepository _repository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public TransactionController(ApiDbContext context, ILogger<TransactionController> logger, TransactionRepository repository): base(context, logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<TransactionRecord>> GetTransactions(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]bool count,
            [FromQuery]string description,
            [FromQuery]List<long> banks,
            [FromQuery]List<long> users,
            [FromQuery]List<long> categories,
            [FromQuery]List<string> years 
        ) 
        {
            var records =  _repository.Filter(_context.TransactionRecord, description, categories, banks, users, years);
            records = _repository.Include(_repository.Order(records, sortName));
            var paginatedList = PaginatedList<TransactionRecord>.Create(records, pageNumber, pageSize);
            return Ok(new PaginatedTransactionResult(paginatedList, paginatedList.TotalCount, records.Sum(t => t.Amount)));
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TransactionRecord> GetTransaction(long id)
        {
            return Ok(_repository.Find(id));
        }

        [HttpPost]
        public ActionResult<TransactionRecord> CreateTransaction([FromBody] TransactionRecord item)
        {
            item.Bank = _context.Bank.First(a => a.Id == item.Bank.Id);
            item.Category = _context.TransactionCategory.First(a => a.Id == item.Category.Id);
            item.User = _context.User.First(a => a.Id == item.User.Id);
 
            return Create<TransactionRecord>(item, nameof(CreateTransaction));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(long id, [FromBody] TransactionRecord item)
        {
            return Update<TransactionRecord>(id, item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(long id)
        {
            return Delete<TransactionRecord>(id);

        }

   }
}
