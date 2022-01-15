using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/foodquantitytype")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FoodQuantityTypeController: LookupControllerBase<FoodQuantityType>
    {

        public FoodQuantityTypeController(AppDbContext context, ILogger<FoodQuantityTypeController> logger,  LookupRepository lookupRepository)
        : base(context, context.FoodQuantityTypes, logger, lookupRepository)
        {
       }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(FoodQuantityType))]
        new public ActionResult<FoodQuantityType>  Update(int id, [FromBody] FoodQuantityType item)
        {
            return base.Update(id, item);
        }

    }
}