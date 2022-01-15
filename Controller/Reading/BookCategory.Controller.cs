using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/bookCategories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookCategoryController: LookupControllerBase<BookCategory>
    {

        public BookCategoryController(AppDbContext context, ILogger<BookCategoryController> logger,  LookupRepository lookupRepository)
        : base(context, context.BookCategories, logger, lookupRepository)
        {
       }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BookCategory))]
        new public ActionResult<BookCategory>  Update(int id, [FromBody] BookCategory item)
        {
            return base.Update(id, item);
        }


    }
}