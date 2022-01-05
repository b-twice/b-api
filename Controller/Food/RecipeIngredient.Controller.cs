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

    [Route("v1/food/recipeIngredients")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RecipeIngredientController: AppControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public RecipeIngredientController(AppDbContext context, ILogger<RecipeIngredientController> logger): base(context, logger)
        {
            _context = context;
            _logger = logger;
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
