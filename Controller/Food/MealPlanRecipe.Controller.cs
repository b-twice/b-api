using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

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

            return Create<MealPlanRecipe>(item, nameof(Create), (long id ) => _mealPlanRecipeRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MealPlanRecipe))]
        public ActionResult<MealPlanRecipe> Update(long id, [FromBody] MealPlanRecipe item)
        {

            return Update<MealPlanRecipe>(id, item, (long id ) => _mealPlanRecipeRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlanRecipe>(id);

        }

   }
}
