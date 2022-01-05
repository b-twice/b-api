using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;

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
    }
}