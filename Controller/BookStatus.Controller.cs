using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Route("reading/statuses")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookStatusController: AppControllerBase
    {
        private readonly LookupRepository _lookupRepository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public BookStatusController(ApiDbContext context, ILogger<BookStatusController> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookStatuses>> GetStatuses()
        {
            return Ok(_context.BookStatuses);
        }
        [Authorize]
        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookStatuses>> GetStatusesPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber = 1,
            [FromQuery]int pageSize = 25
        ) 
        {
            var items = _lookupRepository.OrderBy<BookStatuses>(_context.BookStatuses, sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookStatuses> GetStatus(int id)
        {
            return Ok(_context.BookStatuses.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<BookStatuses> CreateStatus([FromBody] BookStatuses item)
        {
            return Create<BookStatuses>(item, nameof(CreateStatus));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] BookStatuses item)
        {
            return Update<BookStatuses>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteStatus (int id)
        {
            return Delete<BookStatuses>(id);
        }
 

    }
}