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

    [Route("v1/food/recipes")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RecipeController: AppControllerBase
    {
        private readonly RecipeRepository _recipeRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public RecipeController(AppDbContext context, ILogger<RecipeController> logger, RecipeRepository recipeRepository): base(context, logger)
        {
            _recipeRepository = recipeRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Recipe>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name,
            [FromQuery]List<long> users,
            [FromQuery]List<long> categories,
            [FromQuery]List<long> cookbooks,
            [FromQuery]List<long> products
        ) 
        {
            var recipes = _recipeRepository.Filter(_context.Recipes, users, categories, cookbooks, products, name);
            recipes = _recipeRepository.Include(_recipeRepository.Order(recipes, sortName));
            var paginatedList = PaginatedList<Recipe>.Create(recipes, pageNumber, pageSize);
            return Ok(new PaginatedResult<Recipe>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Recipe>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var recipes =_recipeRepository.FindAll().OrderBy(b => b.Name);
            if (size > 0) {
                return Ok(recipes.Take(size));
            }
            return Ok(recipes);
        }

        [HttpGet("recent")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Recipe>> GetRecent(
            [FromQuery]int size 
        ) 
        {
            var recipes =_recipeRepository.FindAll().OrderByDescending(b => b.Id);
            if (size > 0) {
                return Ok(recipes.Take(size));
            }
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Recipe> Get(long id)
        {
            return Ok(_recipeRepository.Find(id));
        }



        [Authorize]
        [HttpPost]
        public ActionResult<Recipe> Create([FromBody] Recipe item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.User).State = EntityState.Unchanged;
            _context.Entry(item.Cookbook).State = EntityState.Unchanged;
            _context.Entry(item.RecipeCategory).State = EntityState.Unchanged;
 
            return Create<Recipe>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Recipe item)
        {
            item.RecipeCategoryId = item?.RecipeCategory?.Id ?? default(int);
            item.UserId = item?.User?.Id ?? default(int);
            item.CookbookId = item?.Cookbook?.Id ?? default(int);

            return Update<Recipe>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<Recipe>(id);

        }

   }
}
