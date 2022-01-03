using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/food/mealplanrecipes")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MealPlanRecipeController: AppControllerBase
    {
        private readonly MealPlanRecipeRepository _mealPlanRecipeRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public MealPlanRecipeController(AppDbContext context, ILogger<MealPlanRecipeController> logger, MealPlanRecipeRepository mealPlanRecipeRepository): base(context, logger)
        {
            _mealPlanRecipeRepository = mealPlanRecipeRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<MealPlanRecipe>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]List<long> mealPlans,
            [FromQuery]List<long> recipes
        ) 
        {
            var mealPlanRecipes = _mealPlanRecipeRepository.Filter(_context.MealPlanRecipes, mealPlans, recipes);
            mealPlanRecipes = _mealPlanRecipeRepository.Include(_mealPlanRecipeRepository.Order(mealPlanRecipes, sortName));
            var paginatedList = PaginatedList<MealPlanRecipe>.Create(mealPlanRecipes, pageNumber, pageSize);
            return Ok(new PaginatedResult<MealPlanRecipe>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlanRecipe>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var mealPlanRecipes =_mealPlanRecipeRepository.FindAll().OrderBy(b => b.MealPlan.Name);
            if (size > 0) {
                return Ok(mealPlanRecipes.Take(size));
            }
            return Ok(mealPlanRecipes);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<MealPlanRecipe> Get(long id)
        {
            return Ok(_mealPlanRecipeRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<MealPlanRecipe> Create([FromBody] MealPlanRecipe item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.MealPlan).State = EntityState.Unchanged;
            _context.Entry(item.Recipe).State = EntityState.Unchanged;
 
            return Create<MealPlanRecipe>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] MealPlanRecipe item)
        {
            item.MealPlanId = item?.MealPlan?.Id ?? default(int);
            item.RecipeId = item?.Recipe?.Id ?? default(int);

            return Update<MealPlanRecipe>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlanRecipe>(id);

        }

   }
}
