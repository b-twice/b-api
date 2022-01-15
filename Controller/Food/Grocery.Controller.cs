using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/food/groceries")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GroceryController: AppControllerBase
    {
        private readonly MealPlanRepository _mealPlanRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public GroceryController(AppDbContext context, ILogger<GroceryController> logger, MealPlanRepository mealPlanRepository): base(context, logger)
        {
            _mealPlanRepository = mealPlanRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlanGrocery>> GetGroceries (int id)
        {
            return Ok(
                _context.MealPlanGroceries.Where(o => o.MealPlanId == id).OrderBy(o => o.Name)
            );
        }

        [HttpGet("mealPlans")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlan>> GetAll() 
        {
            return Ok(_mealPlanRepository.FindAll().OrderByDescending(b => b.Id));
        }



        [HttpGet("mealPlans/latest")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlan>> GetLatest() 
        {
            return Ok(_mealPlanRepository.FindAll().OrderByDescending(o => o.Id).FirstOrDefault());
        }



        [HttpGet("mealPlans/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<MealPlan> Get(long id)
        {
            return Ok(_mealPlanRepository.Find(id));
        }


        [HttpGet("mealPlans/{id}/recipes")]
        public ActionResult<IEnumerable<MealPlanRecipesView>> GetRecipes (int id)
        {
            return Ok(
                _context.MealPlanRecipesViews.AsNoTracking().Where(o => o.MealPlanId == id)
            );
        }

        [HttpGet("recipes/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<MealPlanRecipesView> GetGroceriesRecipe(long id)
        {
            return Ok(_context.MealPlanRecipesViews.AsNoTracking().FirstOrDefault(r => r.RecipeId == id));
        }

        [HttpGet("recipes/{id}/notes")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<RecipeNote>> GetRecipeNotes(long id)
        {
            return Ok(_context.RecipeNotes.AsNoTracking().Where(r => r.RecipeId == id));
        }

        [HttpGet("recipes/{id}/ingredients")]
        public IActionResult GetRecipeIngredients(int id)
        {
            return Ok(_context.RecipeIngredientsViews.AsNoTracking().Where(o => o.RecipeId == id));
        }

   }
}
