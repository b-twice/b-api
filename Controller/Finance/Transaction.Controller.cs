using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Authorize]
    [Route("v1/finance/transactions")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionController: AppControllerBase
    {
        private readonly TransactionRepository _repository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public TransactionController(AppDbContext context, ILogger<TransactionController> logger, TransactionRepository repository): base(context, logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedTransactionResult> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]bool count,
            [FromQuery]string description,
            [FromQuery]List<long> banks,
            [FromQuery]List<long> users,
            [FromQuery]List<long> categories,
            [FromQuery]List<long> tags,
            [FromQuery]List<string> years,
            [FromQuery]List<string>  months
        ) 
        {
            var records =  _repository.Filter(_context.TransactionRecords, description, categories, tags, banks, users, years, months);
            records = _repository.Include(_repository.Order(records, sortName));
            var paginatedList = PaginatedList<TransactionRecord>.Create(records, pageNumber, pageSize);
            return Ok(new PaginatedTransactionResult(paginatedList, paginatedList.TotalCount, records.Sum(t => t.Amount)));
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TransactionRecord> Get(long id)
        {
            return Ok(_repository.Find(id));
        }

        [HttpPost]
        public ActionResult<TransactionRecord> Create([FromBody] TransactionRecord item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.Bank).State = EntityState.Unchanged;
            _context.Entry(item.Category).State = EntityState.Unchanged;
            _context.Entry(item.User).State = EntityState.Unchanged;

            foreach (TransactionRecordTag tag in item.TransactionRecordTags) {
                _context.Entry(tag).State = EntityState.Added;
            }
 
 
            return Create<TransactionRecord>(item, nameof(Create));
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TransactionRecord item)
        {
            _context.Entry(item);
            item.UserId = item?.User?.Id ?? default(int);
            item.CategoryId = item?.Category?.Id ?? default(int);
            item.BankId  = item?.Bank?.Id ?? default(int);

            var existingTags = _context.TransactionRecordTags.Where(t => t.TransactionRecordId == id).ToList();
            var existingTagIds = existingTags.Select(t => t.Id).ToList();
            foreach (var tag in item.TransactionRecordTags)  {
                if (existingTagIds.Contains(tag.Id)) {
                    var existingEntry  = _context.Entry(existingTags.Single(t => t.Id == tag.Id));
                    existingEntry.CurrentValues.SetValues(tag);
                    existingEntry.State = EntityState.Modified;
                }
                else {
                    tag.Id = 0;
                    var newEntry = _context.Entry(tag);
                    newEntry.State = EntityState.Added;
                    existingTags.Add(tag);
                }
            }
            var deleteTags = existingTags.Where(old => !item.TransactionRecordTags.Any(t => t.Id == old.Id));
            _context.TransactionRecordTags.RemoveRange(deleteTags);

            return Update<TransactionRecord>(id, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

            _context.RemoveRange(_context.TransactionRecordTags.Where(t => t.TransactionRecordId == id));
            return Delete<TransactionRecord>(id);

        }

   }
}
