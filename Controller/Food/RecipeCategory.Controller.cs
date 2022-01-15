using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/recipecategories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RecipeCategoryController: LookupControllerBase<RecipeCategory>
    {

        public RecipeCategoryController(AppDbContext context, ILogger<RecipeCategoryController> logger,  LookupRepository lookupRepository)
        : base(context, context.RecipeCategories, logger, lookupRepository)
        {
       }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RecipeCategory))]
        new public ActionResult<RecipeCategory>  Update(int id, [FromBody] RecipeCategory item)
        {
            return base.Update(id, item);
        }
    }
}