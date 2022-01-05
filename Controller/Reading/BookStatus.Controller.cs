using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;

namespace B.API.Controller
{

    [Route("v1/food/bookStatuses")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookStatusController: LookupControllerBase<BookStatus>
    {

        public BookStatusController(AppDbContext context, ILogger<BookStatusController> logger,  LookupRepository lookupRepository)
        : base(context, context.BookStatuses, logger, lookupRepository)
        {
       }
    }
}