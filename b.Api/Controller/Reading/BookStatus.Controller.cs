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

  [Route("v1/reading/statuses")]
  [ApiController]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class BookStatusController : AppControllerBase
  {
    private readonly LookupRepository _lookupRepository;
    private readonly AppDbContext _context;

    private readonly ILogger _logger;
    public BookStatusController(AppDbContext context, ILogger<BookStatusController> logger, LookupRepository lookupRepository) : base(context, logger)
    {
      _lookupRepository = lookupRepository;
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<BookStatus>> GetStatuses()
    {
      return Ok(_context.BookStatuses.AsNoTracking());
    }
    [Authorize]
    [HttpGet("page")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<PaginatedResult<BookStatus>> GetStatusesPage(
        [FromQuery] string sortName,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 25
    )
    {
      var items = _lookupRepository.OrderBy<BookStatus>(_context.BookStatuses.AsNoTracking(), sortName);
      return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
    }
    [Authorize]
    [HttpGet("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<BookStatus> GetStatus(int id)
    {
      return Ok(_context.BookStatuses.AsNoTracking().First(o => o.Id == id));
    }
    [Authorize]
    [HttpPost]
    public ActionResult<BookStatus> CreateStatus([FromBody] BookStatus item)
    {
      return Create<BookStatus>(item, nameof(CreateStatus));
    }
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateStatus(int id, [FromBody] BookStatus item)
    {
      return Update<BookStatus>(id, item);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteStatus(int id)
    {
      return Delete<BookStatus>(id);
    }


  }
}