using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using b.Api.Entities;
using b.Entities.Food;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Budget.Core;

namespace b.Api
{

  [Authorize]
  [Route("food/recipes")]
  public class RecipeController : ControllerBase
  {
    private readonly DatabaseContext _context;

    private readonly ILogger _logger;
    public RecipeController(DatabaseContext context, ILogger<RecipeController> logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet("categories")]
    public IActionResult GetRecipeCategories()
    {
      return Ok(_context.RecipeCategories.ToList().OrderBy(c => c.name));
    }

    [HttpGet()]
    public IActionResult GetRecipes()
    {
      return Ok(_context.Recipes.ToList());
    }

    [HttpGet("user/{name}")]
    public IActionResult GetRecipes(string name, [FromQuery] List<string> categoryNames)
    {
      if (name == "All")
      {

        return Ok(_context.Recipes.Where(o => categoryNames.Count > 0 ? categoryNames.Exists(c => o.category == c) : true));
      }
      return Ok(_context.Recipes.Where(o => o.user == name && (categoryNames.Count > 0 ? categoryNames.Exists(c => o.category == c) : true)));
    }

    [HttpGet("user/{name}/{ingredient}")]
    public IActionResult GetRecipesWithIngredient(string name, string ingredient, [FromQuery] List<string> categoryNames)
    {
      if (name == "All")
      {
        return Ok(
            from recipe in _context.Recipes
            join recipeIngredient in _context.RecipeIngredients on recipe.name equals recipeIngredient.recipe
            where
                (categoryNames.Count > 0 ? categoryNames.Contains(recipe.category) : true)
                && recipeIngredient.name.Contains(ingredient)
            group recipe by recipe.id into r
            select r.First()
        );
      }
      return Ok(
          from recipe in _context.Recipes
          join recipeIngredient in _context.RecipeIngredients on recipe.name equals recipeIngredient.recipe
          where
              recipe.user == name
              && (categoryNames.Count > 0 ? categoryNames.Contains(recipe.category) : true)
              && recipeIngredient.name.Contains(ingredient)
          group recipe by recipe.id into r
          select r.First()
      );

    }


    [HttpGet("user/{name}/category/{category}")]
    public IActionResult GetRecipes(string name, string category)
    {
      return Ok(_context.Recipes.Where(o => o.user == name && o.category == category));
    }

    [HttpGet("recipe/{id}")]
    public IActionResult GetRecipe(int id)
    {
      return Ok(_context.Recipes.First(o => o.id == id));
    }

    [HttpGet("ingredient/{id}")]
    public IActionResult GetRecipeIngredient(int id)
    {
      return Ok(_context.RecipeIngredients.First(o => o.id == id));
    }


    [HttpGet("ingredients/{id}")]
    public IActionResult GetRecipeIngredients(int id)
    {
      var recipe = _context.Recipes.FirstOrDefault(t => t.id == id);
      return Ok(_context.RecipeIngredients.Where(o => o.recipe == recipe.name));
    }

    [HttpPost("recipe")]
    public IActionResult InsertRecipe([FromBody] Recipe item)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }
      try
      {
        _context.Database.ExecuteSqlInterpolated(
             $@"INSERT INTO RecipesView(user, category, cookbook, name, servings, page_number, url)
                    VALUES 
                        ({item.user}, {item.category}, {item.cookbook}, {item.name}, {item.servings}, {item.pageNumber}, {item.url});
                    ");
      }
      catch (Exception ex)
      {
        _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return Ok(_context.Recipes.First(o => o.name == item.name));
    }

    [HttpPut("recipe/{id}")]
    public IActionResult UpdateRecipe(int id, [FromBody] Recipe item)
    {
      if (!ModelState.IsValid || item == null || item.id != id)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
        return BadRequest();
      }

      var recipe = _context.Recipes.FirstOrDefault(t => t.id == id);
      if (recipe == null)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"UPDATE(id) NOT FOUND");
        return NotFound();
      }
      try
      {
        _context.Database.ExecuteSqlInterpolated(
            $@"UPDATE RecipesView SET
                        user = {item.user},
                        category = {item.category},
                        cookbook = {item.cookbook},
                        name = {item.name},
                        servings = {item.servings},
                        page_number = {item.pageNumber},
                        url = {item.id}
                    WHERE id = {item.url};
                ");
      }
      catch (Exception ex)
      {
        _logger.LogError(LoggingEvents.UpdateItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpDelete("recipe/{id}")]
    public IActionResult DeleteRecipe(int id)
    {
      var recipe = _context.Recipes.FirstOrDefault(t => t.id == id);
      if (recipe == null)
      {
        _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
        return NotFound();
      }

      try
      {
        _context.Database.ExecuteSqlInterpolated($@"DELETE FROM RecipesView WHERE id = {recipe.id};");
      }
      catch (Exception ex)
      {
        _logger.LogError(LoggingEvents.DeleteItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpPost("ingredients")]
    public IActionResult PostIngredients([FromBody] IngredientList list)
    {
      if (!ModelState.IsValid || list.ingredients.Count == 0)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }

      try
      {

        foreach (RecipeIngredient item in list.ingredients)
        {
          _context.Database.ExecuteSqlInterpolated(
              $@"INSERT INTO RecipeIngredientsView(recipe, name, count, weight, measurement)
                        VALUES 
                            ({item.recipe}, {item.name}, {item.count}, {item.weight}, {item.measurement});
                        ");
        }
      }
      catch (Exception ex)
      {
        _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }


    [HttpPost("ingredient")]
    public IActionResult InsertRecipeIngredient([FromBody] RecipeIngredient item)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }
      try
      {
        _context.Database.ExecuteSqlInterpolated(
             $@"INSERT INTO RecipeIngredientsView(recipe, name, count, weight, measurement)
                    VALUES 
                    ({item.recipe}, {item.name}, {item.count}, {item.weight}, {item.measurement});
                ");
      }
      catch (Exception ex)
      {
        _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpPut("ingredient/{id}")]
    public IActionResult UpdateRecipeIngredient(int id, [FromBody] RecipeIngredient item)
    {
      if (!ModelState.IsValid || item == null || item.id != id)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
        return BadRequest();
      }

      var recipeIngredient = _context.RecipeIngredients.FirstOrDefault(t => t.id == id);
      if (recipeIngredient == null)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"UPDATE(id) NOT FOUND");
        return NotFound();
      }


      try
      {
        _context.Database.ExecuteSqlInterpolated(
            $@"UPDATE RecipeIngredientsView SET
                        recipe = {item.recipe},
                        name = {item.name},
                        count = {item.count},
                        weight = {item.weight},
                        measurement= {item.measurement}
                    WHERE id = {item.id};
                    ");
      }
      catch (Exception ex)
      {
        _logger.LogError(LoggingEvents.UpdateItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpDelete("ingredient/{id}")]
    public IActionResult DeleteRecipeIngredient(int id)
    {
      var ingredient = _context.RecipeIngredients.FirstOrDefault(t => t.id == id);
      if (ingredient == null)
      {
        _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
        return NotFound();
      }

      try
      {
        _context.Database.ExecuteSqlInterpolated($@"DELETE FROM RecipeIngredientsView WHERE id = {ingredient.id};");
      }
      catch (Exception ex)
      {
        _logger.LogError(LoggingEvents.DeleteItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

  }

}
