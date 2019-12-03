using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Route("reading/books")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookController: AppControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public BookController(ApiDbContext context, ILogger<BookController> logger, BookRepository bookRepository): base(context, logger)
        {
            _bookRepository = bookRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Books>> GetBooksPage(
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


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Books>> GetBooks(
            [FromQuery]int size 
        ) 
        {
            // NOTE - This is not an exact approach, just takes the last added, since we only store the year and not a date. 
            // Considering I've added all the books I've already read this, I should only be adding ones I actually just read.
            // If the order is slight out of sync with reality, not that big of a deal            
            var books =_bookRepository.FindAll().OrderByDescending(b => b.Id);
            if (size > 0) {
                return Ok(books.Take(size));
            }
            return Ok(books);
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

   }
}
