using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/supermarkets")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SupermarketController: LookupControllerBase<Supermarket>
    {

        public SupermarketController(AppDbContext context, ILogger<SupermarketController> logger,  LookupRepository lookupRepository)
        : base(context, context.Supermarkets, logger, lookupRepository)
        {
       }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Supermarket))]
        new public ActionResult<Supermarket>  Update(int id, [FromBody] Supermarket item)
        {
            return base.Update(id, item);
        }

    }
}