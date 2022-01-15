using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

namespace B.API.Controller
{

    [Route("v1/reading/books")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookController: AppControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public BookController(AppDbContext context, ILogger<BookController> logger, BookRepository bookRepository): base(context, logger)
        {
            _bookRepository = bookRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Book>> GetPage(
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
            var books = _bookRepository.Filter(_context.Books.AsNoTracking(), bookName, bookAuthors, bookCategories, bookStatuses, readYears);
            books = _bookRepository.Order(_bookRepository.Include(books), sortName);
            var paginatedList = PaginatedList<Book>.Create(books, pageNumber, pageSize);
            return Ok(new PaginatedResult<Book>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Book>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var books =_bookRepository.FindAll().OrderByDescending(b => b.ReadDate);
            if (size > 0) {
                return Ok(books.Take(size));
            }
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Book> Get(long id)
        {
            return Ok(_bookRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book item)
        {

            return Create<Book>(item, nameof(Create), (long id) => _bookRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        public ActionResult<Book> Update(long id, [FromBody] Book item)
        {
            return Update<Book>(id, item, (long id) => _bookRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<Book>(id);

        }

   }
}


