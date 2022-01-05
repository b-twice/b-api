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

    [Route("v1/food/mealplans")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MealPlanController: AppControllerBase
    {
        private readonly MealPlanRepository _mealPlanRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public MealPlanController(AppDbContext context, ILogger<MealPlanController> logger, MealPlanRepository mealPlanRepository): base(context, logger)
        {
            _mealPlanRepository = mealPlanRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<MealPlan>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name,
            [FromQuery]List<long> users,
            [FromQuery]List<long> recipes
        ) 
        {
            var mealPlans = _mealPlanRepository.Filter(_context.MealPlans, users, recipes, name);
            mealPlans = _mealPlanRepository.Include(_mealPlanRepository.Order(mealPlans, sortName));
            var paginatedList = PaginatedList<MealPlan>.Create(mealPlans, pageNumber, pageSize);
            return Ok(new PaginatedResult<MealPlan>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlan>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var mealPlans =_mealPlanRepository.FindAll().OrderByDescending(b => b.Name);
            if (size > 0) {
                return Ok(mealPlans.Take(size));
            }
            return Ok(mealPlans);
        }

        [HttpGet("latest")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlan>> GetLatest() 
        {
            var mealPlans =_mealPlanRepository.FindAll().OrderByDescending(o => o.Id);
            return Ok(mealPlans.FirstOrDefault());
        }



        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<MealPlan> Get(long id)
        {
            return Ok(_mealPlanRepository.Find(id));
        }

        [HttpGet("{id}/groceries")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlanGrocery>> GetGroceries (int id)
        {
            return Ok(
                _context.MealPlanGroceries.Where(o => o.MealPlanId == id).OrderBy(o => o.Name)
            );
        }

        [HttpGet("{id}/recipes")]
        public ActionResult<IEnumerable<MealPlanRecipe>> GetRecipes (int id)
        {
            return Ok(
                _context.MealPlanRecipesViews.Where(o => o.MealPlanId == id)
            );
        }


        [Authorize]
        [HttpPost]
        public ActionResult<MealPlan> Create([FromBody] MealPlan item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.User).State = EntityState.Unchanged;
            foreach (MealPlanRecipe recipe in item.MealPlanRecipes) {
                _context.Entry(recipe).State = EntityState.Added;
            }
 
            return Create<MealPlan>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] MealPlan item)
        {
            _context.Entry(item);
            item.UserId = item?.User?.Id ?? default(int);
            var existingRecipes = _context.MealPlanRecipes.Where(t => t.MealPlanId == id).ToList();
            var existingRecipeIds = existingRecipes.Select(t => t.Id).ToList();
            foreach (var tag in item.MealPlanRecipes)  {
                if (existingRecipeIds.Contains(tag.Id)) {
                    var existingEntry  = _context.Entry(existingRecipes.Single(t => t.Id == tag.Id));
                    existingEntry.CurrentValues.SetValues(tag);
                    existingEntry.State = EntityState.Modified;
                }
                else {
                    tag.Id = 0;
                    var newEntry = _context.Entry(tag);
                    newEntry.State = EntityState.Added;
                    existingRecipes.Add(tag);
                }
            }
            var deleteRecipes = existingRecipes.Where(old => !item.MealPlanRecipes.Any(t => t.Id == old.Id));
            _context.MealPlanRecipes.RemoveRange(deleteRecipes);

 
            return Update<MealPlan>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlan>(id);

        }

   }
}
