using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/bookAuthors")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookAuthorController: LookupControllerBase<BookAuthor>
    {

        public BookAuthorController(AppDbContext context, ILogger<BookAuthorController> logger,  LookupRepository lookupRepository)
        : base(context, context.BookAuthors, logger, lookupRepository)
        {
       }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BookAuthor))]
        new public ActionResult<BookAuthor>  Update(int id, [FromBody] BookAuthor item)
        {
            return base.Update(id, item);
        }

    }
}