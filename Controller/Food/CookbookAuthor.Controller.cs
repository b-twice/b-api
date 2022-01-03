using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using B.API.Models;
using Microsoft.Extensions.Logging;

namespace B.API.Controller
{

    [Route("v1/food/cookbookauthors")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CookbookAuthorController: LookupControllerBase<CookbookAuthor>
    {

        public CookbookAuthorController(AppDbContext context, ILogger<CookbookAuthorController> logger,  LookupRepository lookupRepository)
        : base(context, context.CookbookAuthors, logger, lookupRepository)
        {
       }
    }
}