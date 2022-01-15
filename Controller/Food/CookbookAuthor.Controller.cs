using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CookbookAuthor))]
        new public ActionResult<CookbookAuthor>  Update(int id, [FromBody] CookbookAuthor item)
        {
            return base.Update(id, item);
        }

    }
}