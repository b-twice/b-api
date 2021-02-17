using System.Linq;
using Microsoft.AspNetCore.Mvc;
using b.Api.Database;
using Microsoft.AspNetCore.Authorization;
using b.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace b.Api.Controller
{

  [Route("v1/finance/transaction-tags")]
  [ApiController]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class TransactionTagController : AppControllerBase
  {
    private readonly LookupRepository _lookupRepository;
    private readonly AppDbContext _context;

    private readonly ILogger _logger;
    public TransactionTagController(AppDbContext context, ILogger<TransactionTagController> logger, LookupRepository lookupRepository) : base(context, logger)
    {
      _lookupRepository = lookupRepository;
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<TransactionTag>> GetTransactionTags()
    {
      return Ok(_context.TransactionTags.AsNoTracking().OrderBy(c => c.Name));
    }

    [Authorize]
    [HttpGet("page")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<PaginatedResult<TransactionTag>> GetTransactionTagsPage(
        [FromQuery] string sortName,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string name
    )
    {
      var items = _lookupRepository.OrderBy<TransactionTag>(_lookupRepository.Filter<TransactionTag>(_context.TransactionTags.AsNoTracking(), name), sortName);
      return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
    }
    [Authorize]
    [HttpGet("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<TransactionTag> GetTransactionTag(int id)
    {
      return Ok(_context.TransactionTags.AsNoTracking().First(o => o.Id == id));
    }
    [Authorize]
    [HttpPost]
    public ActionResult<TransactionTag> CreateTransactionTag([FromBody] TransactionTag item)
    {
      return Create<TransactionTag>(item, nameof(CreateTransactionTag));
    }
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateTransactionTag(int id, [FromBody] TransactionTag item)
    {
      return Update<TransactionTag>(id, item);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteTransactionTag(int id)
    {
      return Delete<TransactionTag>(id);
    }



  }

}