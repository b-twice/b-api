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
            return Create<RecipeIngredient>(item, nameof(Create), (long id) => _context.RecipeIngredients.AsNoTracking().Include(o => o.FoodProduct).First(o => o.Id == id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RecipeIngredient))]
        public ActionResult<RecipeIngredient> Update(long id, [FromBody] RecipeIngredient item)
        {
            return Update<RecipeIngredient>(id, item, (long id) => _context.RecipeIngredients.AsNoTracking().Include(o => o.FoodProduct).First(o => o.Id == id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<RecipeIngredient>(id);

        }

   }
}
