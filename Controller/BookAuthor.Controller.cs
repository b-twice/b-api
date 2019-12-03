using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Route("reading/authors")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookAuthorController: AppControllerBase
    {
        private readonly LookupRepository _lookupRepository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public BookAuthorController(ApiDbContext context, ILogger<BookAuthorController> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookAuthors>> GetAuthors()
        {
            return Ok(_context.BookAuthors.OrderBy(c => c.Name));
        }

        [Authorize]
        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookAuthors>> GetAuthorsPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name
        ) 
        {
            var items = _lookupRepository.OrderBy(_lookupRepository.Filter(_context.BookAuthors, name), sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookAuthors> GetAuthor(int id)
        {
            return Ok(_context.BookAuthors.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<BookAuthors> CreateAuthor([FromBody] BookAuthors item)
        {
            return Create<BookAuthors>(item, nameof(CreateAuthor));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] BookAuthors item)
        {
            return Update<BookAuthors>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor (int id)
        {
            return Delete<BookAuthors>(id);
        }



    }

}