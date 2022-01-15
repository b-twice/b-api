using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

namespace B.API.Controller
{

    [Route("v1/food/cookbooks")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CookbookController: AppControllerBase
    {
        private readonly CookbookRepository _bookRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public CookbookController(AppDbContext context, ILogger<BookController> logger, CookbookRepository bookRepository): base(context, logger)
        {
            _bookRepository = bookRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Cookbook>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string bookName,
            [FromQuery]List<long> bookAuthors
        ) 
        {
            var books = _bookRepository.Filter(_context.Cookbooks, bookName, bookAuthors);
            books = _bookRepository.Include(_bookRepository.Order(books, sortName));
            var paginatedList = PaginatedList<Cookbook>.Create(books, pageNumber, pageSize);
            return Ok(new PaginatedResult<Cookbook>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Cookbook>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var books =_bookRepository.FindAll().OrderByDescending(b => b.Name);
            if (size > 0) {
                return Ok(books.Take(size));
            }
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Cookbook> Get(long id)
        {
            return Ok(_bookRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Cookbook> Create([FromBody] Cookbook item)
        {
            return Create<Cookbook>(item, nameof(Create), (long id) => _bookRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Cookbook))]
        public ActionResult<Cookbook> Update(long id, [FromBody] Cookbook item)
        {
            return Update<Cookbook>(id, item, (long id) => _bookRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<Cookbook>(id);

        }

   }
}
