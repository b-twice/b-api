using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Database;
using Budget.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Budget.API.Models.Book;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Budget.API 
{

    [Route("Books")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookController: AppControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly LookupRepository _lookupRepository;
        private readonly AppDatabaseContext _context;

        private readonly ILogger _logger;
        public BookController(AppDatabaseContext context, ILogger<BookController> logger, BookRepository bookRepository, LookupRepository lookupRepository): base(context, logger)
        {
            _bookRepository = bookRepository;
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Book>> GetBooks(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string bookName,
            [FromQuery]List<int> bookAuthors,
            [FromQuery]List<int> bookCategories,
            [FromQuery]List<int> bookStatuses,
            [FromQuery]List<string> readYears


        ) 
        {
            var books = _bookRepository.Filter(_context.Books, bookName, bookAuthors, bookCategories, bookStatuses, readYears);
            books = _bookRepository.Include(_bookRepository.Order(books, sortName));
            var paginatedList = PaginatedList<Book>.Create(books, pageNumber, pageSize);
            return Ok(new PaginatedResult<Book>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet("recent")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Book>> GetRecentBooks() 
        {
            return Ok(
                // NOTE - This is not an exact approach, just takes the last added, since we only store the year and not a date. 
                // Considering I've added all the books I've already read this, I should only be adding ones I actually just read.
                // If the order is slight out of sync with reality, not that big of a deal
                _bookRepository.FindAll().OrderByDescending(b => b.id).Take(5)
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Book> GetBook(int id)
        {
            return Ok(_bookRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book item)
        {
            item.bookAuthor = _context.BookAuthors.First(a => a.id == item.bookAuthor.id);
            item.bookCategory = _context.BookCategories.First(a => a.id == item.bookCategory.id);
            item.bookStatus  = _context.BookStatuses.First(a => a.id == item.bookStatus.id);
 
            return Create<Book>(item, nameof(CreateBook));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book item)
        {
            return Update<Book>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
            return Delete<Book>(id);

        }

        [HttpGet("Authors")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookAuthor>> GetAuthors()
        {
            return Ok(_context.BookAuthors.OrderBy(c => c.name));
        }
        [Authorize]
        [HttpGet("Authors/Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookAuthor>> GetAuthorsPage(
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
        public ActionResult<BookAuthor> GetAuthor(int id)
        {
            return Ok(_context.BookAuthors.First(o => o.id == id));
        }
        [Authorize]
        [HttpPost("Authors")]
        public ActionResult<BookAuthor> CreateAuthor([FromBody] BookAuthor item)
        {
            return Create<BookAuthor>(item, nameof(CreateAuthor));
        }
        [Authorize]
        [HttpPut("Authors/{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] BookAuthor item)
        {
            return Update<BookAuthor>(id, item);
        }
        [Authorize]
        [HttpDelete("Authors/{id}")]
        public IActionResult DeleteAuthor (int id)
        {
            return Delete<BookAuthor>(id);
        }

        [HttpGet("Categories")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookCategory>> GetCategories()
        {
            return Ok(_context.BookCategories.OrderBy(c => c.name));
        }
        [Authorize]
        [HttpGet("Categories/Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookCategory>> GetCategoriesPage(
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
        public ActionResult<BookCategory> GetCategory(int id)
        {
            return Ok(_context.BookCategories.First(o => o.id == id));
        }
        [Authorize]
        [HttpPost("Categories")]
        public ActionResult<BookCategory> CreateCategory([FromBody] BookCategory item)
        {
            return Create<BookCategory>(item, nameof(CreateCategory));
        }
        [Authorize]
        [HttpPut("Categories/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] BookCategory item)
        {
            return Update<BookCategory>(id, item);
        }
        [Authorize]
        [HttpDelete("Categories/{id}")]
        public IActionResult DeleteCategory (int id)
        {
            return Delete<BookCategory>(id);
        }

        [HttpGet("Statuses")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookStatus>> GetStatuses()
        {
            return Ok(_context.BookStatuses);
        }
        [Authorize]
        [HttpGet("Statuses/Page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<BookStatus>> GetStatusesPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber = 1,
            [FromQuery]int pageSize = 25
        ) 
        {
            var items = _lookupRepository.OrderBy(_context.BookStatuses, sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("Statuses/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookStatus> GetStatus(int id)
        {
            return Ok(_context.BookStatuses.First(o => o.id == id));
        }
        [Authorize]
        [HttpPost("Statuses")]
        public ActionResult<BookStatus> CreateStatus([FromBody] BookStatus item)
        {
            return Create<BookStatus>(item, nameof(CreateStatus));
        }
        [Authorize]
        [HttpPut("Statuses/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] BookStatus item)
        {
            return Update<BookStatus>(id, item);
        }
        [Authorize]
        [HttpDelete("Statuses/{id}")]
        public IActionResult DeleteStatus (int id)
        {
            return Delete<BookStatus>(id);
        }
 
   }
}
