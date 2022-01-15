using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/foodunit")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FoodUnitController: LookupControllerBase<FoodUnit>
    {

        public FoodUnitController(AppDbContext context, ILogger<FoodUnitController> logger,  LookupRepository lookupRepository)
        : base(context, context.FoodUnits, logger, lookupRepository)
        {
       }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(FoodUnit))]
        new public ActionResult<FoodUnit>  Update(int id, [FromBody] FoodUnit item)
        {
            return base.Update(id, item);
        }

    }
}