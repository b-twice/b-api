using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using B.API.Models;
using Microsoft.Extensions.Logging;

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
    }
}