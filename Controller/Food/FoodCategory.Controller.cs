using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/foodcategories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FoodCategoryController: LookupControllerBase<FoodCategory>
    {

        public FoodCategoryController(AppDbContext context, ILogger<FoodCategoryController> logger,  LookupRepository lookupRepository)
        : base(context, context.FoodCategories, logger, lookupRepository)
        {
       }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(FoodCategory))]
        new public ActionResult<FoodCategory>  Update(int id, [FromBody] FoodCategory item)
        {
            return base.Update(id, item);
        }

    }
}