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

    [Route("v1/food/recipeIngredients")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RecipeIngredientController: AppControllerBase
    {
        private readonly RecipeIngredientRepository _recipeIngredientRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public RecipeIngredientController(AppDbContext context, ILogger<RecipeIngredientController> logger, RecipeIngredientRepository recipeIngredientRepository): base(context, logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<RecipeIngredient>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]List<long> products,
            [FromQuery]List<long> recipes 
        ) 
        {
            var recipeIngredients = _recipeIngredientRepository.Filter(_context.RecipeIngredients, products, recipes);
            recipeIngredients = _recipeIngredientRepository.Include(_recipeIngredientRepository.Order(recipeIngredients, sortName));
            var paginatedList = PaginatedList<RecipeIngredient>.Create(recipeIngredients, pageNumber, pageSize);
            return Ok(new PaginatedResult<RecipeIngredient>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<RecipeIngredient>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var recipeIngredients =_recipeIngredientRepository.FindAll().OrderBy(b => b.Recipe.Name);
            if (size > 0) {
                return Ok(recipeIngredients.Take(size));
            }
            return Ok(recipeIngredients);
        }




        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<RecipeIngredient> Get(long id)
        {
            return Ok(_recipeIngredientRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<RecipeIngredient> Create([FromBody] RecipeIngredient item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.Recipe).State = EntityState.Unchanged;
            _context.Entry(item.FoodProduct).State = EntityState.Unchanged;
 
            return Create<RecipeIngredient>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] RecipeIngredient item)
        {
            item.RecipeId = item?.Recipe?.Id ?? default(int);
            item.FoodProductId = item?.FoodProduct?.Id ?? default(int);

            return Update<RecipeIngredient>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<RecipeIngredient>(id);

        }

   }
}
