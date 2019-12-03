using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API 
{

    [Route("Books")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookController: AppControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly LookupRepository _lookupRepository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public BookController(ApiDbContext context, ILogger<BookController> logger, BookRepository bookRepository, LookupRepository lookupRepository): base(context, logger)
        {
            _bookRepository = bookRepository;
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Books>> GetBooks(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string bookName,
            [FromQuery]List<long> bookAuthors,
            [FromQuery]List<long> bookCategories,
            [FromQuery]List<long> bookStatuses,
            [FromQuery]List<string> readYears


        ) 
        {
            var books = _bookRepository.Filter(_context.Books, bookName, bookAuthors, bookCategories, bookStatuses, readYears);
            books = _bookRepository.Include(_bookRepository.Order(books, sortName));
            var paginatedList = PaginatedList<Books>.Create(books, pageNumber, pageSize);
            return Ok(new PaginatedResult<Books>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet("recent")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Books>> GetRecentBooks() 
        {
            return Ok(
                // NOTE - This is not an exact approach, just takes the last added, since we only store the year and not a date. 
                // Considering I've added all the books I've already read this, I should only be adding ones I actually just read.
                // If the order is slight out of sync with reality, not that big of a deal
                _bookRepository.FindAll().OrderByDescending(b => b.Id).Take(5)
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Books> GetBook(int id)
        {
            return Ok(_bookRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Books> CreateBook([FromBody] Books item)
        {
            item.BookAuthor = _context.BookAuthors.First(a => a.Id == item.BookAuthor.Id);
            item.BookCategory = _context.BookCategories.First(a => a.Id == item.BookCategory.Id);
            item.BookStatus  = _context.BookStatuses.First(a => a.Id == item.BookStatus.Id);
 
            return Create<Books>(item, nameof(CreateBook));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Books item)
        {
            return Update<Books>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
            return Delete<Books>(id);

        }

        [HttpGet("Authors")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookAuthors>> GetAuthors()
        {
            return Ok(_context.BookAuthors.OrderBy(c => c.Name));
        }
        [Authorize]
        [HttpGet("Authors/Page")]
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
        [HttpGet("Authors/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookAuthors> GetAuthor(int id)
        {
            return Ok(_context.BookAuthors.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost("Authors")]
        public ActionResult<BookAuthors> CreateAuthor([FromBody] BookAuthors item)
        {
            return Create<BookAuthors>(item, nameof(CreateAuthor));
        }
        [Authorize]
        [HttpPut("Authors/{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] BookAuthors item)
        {
            return Update<BookAuthors>(id, item);
        }
        [Authorize]
        [HttpDelete("Authors/{id}")]
        public IActionResult DeleteAuthor (int id)
        {
            return Delete<BookAuthors>(id);
        }

        [HttpGet("Categories")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookCategories>> GetCategories()
        {
            return Ok(_context.BookCategories.OrderBy(c => c.Name));
        }
        [Authorize]
        [HttpGet("Categories/Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookCategories>> GetCategoriesPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber = 1,
            [FromQuery]int pageSize = 25
        ) 
        {
            var items = _lookupRepository.OrderBy(_context.BookCategories, sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [HttpGet("Categories/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookCategories> GetCategory(int id)
        {
            return Ok(_context.BookCategories.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost("Categories")]
        public ActionResult<BookCategories> CreateCategory([FromBody] BookCategories item)
        {
            return Create<BookCategories>(item, nameof(CreateCategory));
        }
        [Authorize]
        [HttpPut("Categories/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] BookCategories item)
        {
            return Update<BookCategories>(id, item);
        }
        [Authorize]
        [HttpDelete("Categories/{id}")]
        public IActionResult DeleteCategory (int id)
        {
            return Delete<BookCategories>(id);
        }

        [HttpGet("Statuses")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookStatuses>> GetStatuses()
        {
            return Ok(_context.BookStatuses);
        }
        [Authorize]
        [HttpGet("Statuses/Page")]
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
        [HttpGet("Statuses/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookStatuses> GetStatus(int id)
        {
            return Ok(_context.BookStatuses.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost("Statuses")]
        public ActionResult<BookStatuses> CreateStatus([FromBody] BookStatuses item)
        {
            return Create<BookStatuses>(item, nameof(CreateStatus));
        }
        [Authorize]
        [HttpPut("Statuses/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] BookStatuses item)
        {
            return Update<BookStatuses>(id, item);
        }
        [Authorize]
        [HttpDelete("Statuses/{id}")]
        public IActionResult DeleteStatus (int id)
        {
            return Delete<BookStatuses>(id);
        }
 
   }
}
