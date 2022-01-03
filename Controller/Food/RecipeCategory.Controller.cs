using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using B.API.Models;
using Microsoft.Extensions.Logging;

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
    }
}