using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Route("reading/categories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookCategoryController: AppControllerBase
    {
        private readonly LookupRepository _lookupRepository;
        private readonly ApiDbContext _context;

        private readonly ILogger _logger;
        public BookCategoryController(ApiDbContext context, ILogger<BookCategoryController> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<BookCategories>> GetCategories()
        {
            return Ok(_context.BookCategories.OrderBy(c => c.Name));
        }
        [Authorize]
        [HttpGet("page")]
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
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<BookCategories> GetCategory(int id)
        {
            return Ok(_context.BookCategories.First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<BookCategories> CreateCategory([FromBody] BookCategories item)
        {
            return Create<BookCategories>(item, nameof(CreateCategory));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] BookCategories item)
        {
            return Update<BookCategories>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory (int id)
        {
            return Delete<BookCategories>(id);
        }


    }
}

