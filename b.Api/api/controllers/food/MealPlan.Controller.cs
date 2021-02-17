using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using b.Entities.Food;
using b.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Budget.Core;


namespace b.Api
{

  [Authorize]
  [Route("food/meal-plans")]
  public class MealPlanController : ControllerBase
  {
    private readonly DatabaseContext _context;

    private readonly ILogger _logger;
    public MealPlanController(DatabaseContext context, ILogger<MealPlanController> logger)
    {
      _context = context;
      _logger = logger;

    }

    [HttpGet("meal-plan/{id}")]
    public IActionResult GetMealPlan(int id)
    {
      return Ok(
          _context.MealPlans.FirstOrDefault(o => o.id == id)
      );
    }

    [HttpGet("user/{name}")]
    public IActionResult GetMealPlans(string name)
    {
      bool isAll = name == "All" ? true : false;
      return Ok(
          _context.MealPlans.Where(o => isAll || o.user == name)
      );
    }

    [HttpGet("recipes/{id}")]
    public IActionResult GetMealPlanRecipes(int id)
    {
      return Ok(
          _context.MealPlanRecipes.Where(o => o.mealPlanId == id)
      );
    }

    [HttpGet("recipe/{id}")]
    public IActionResult GetMealPlanRecipe(int id)
    {
      return Ok(
          _context.MealPlanRecipes.FirstOrDefault(o => o.id == id)
      );
    }

    [HttpGet("groceries/{id}")]
    public IActionResult GetMealPlanGroceries(int id)
    {
      return Ok(
          _context.MealPlanGroceries.Where(o => o.mealPlanId == id).OrderBy(o => o.name)
      );
    }

    [HttpPost("meal-plan")]
    public IActionResult InsertMealPlan([FromBody] MealPlan item)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }
      try
      {
        _context.Database.ExecuteSqlInterpolated(
             $"INSERT INTO MealPlansView(name, user, days) VALUES  ({item.name}, {item.user}, {item.days});");
      }
      catch (Exception ex)
      {
        _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return Ok(_context.MealPlans.FirstOrDefault(o => o.name == item.name));
    }

    [HttpPut("meal-plan/{id}")]
    public IActionResult UpdateMealPlan(int id, [FromBody] MealPlan item)
    {
      if (!ModelState.IsValid || item == null || item.id != id)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
        return BadRequest();
      }

      var mealPlan = _context.MealPlans.FirstOrDefault(t => t.id == id);
      if (mealPlan == null)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"UPDATE(id) NOT FOUND");
        return NotFound();
      }


      try
      {
        _context.Database.ExecuteSqlInterpolated(
            $@"UPDATE MealPlansView SET
                        name = {item.name},
                        user = {item.user},
                        days = {item.days}
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

    [HttpDelete("meal-plan/{id}")]
    public IActionResult DeleteMealPlan(int id)
    {
      var mealPlan = _context.MealPlans.FirstOrDefault(t => t.id == id);
      if (mealPlan == null)
      {
        _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
        return NotFound();
      }

      try
      {
        _context.Database.ExecuteSqlInterpolated($"DELETE FROM MealPlansView WHERE id = {mealPlan.id};");
      }
      catch (Exception ex)
      {
        _logger.LogError(LoggingEvents.DeleteItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpPost("recipes")]
    public IActionResult InsertMealPlanRecipes([FromBody] MealPlanRecipeList list)
    {
      if (!ModelState.IsValid || list.recipes.Count == 0)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }
      try
      {

        foreach (MealPlanRecipe item in list.recipes)
        {
          _context.Database.ExecuteSqlInterpolated(
              $@"INSERT INTO MealPlanRecipesView(meal_plan_name, name, count)
                        VALUES 
                            ({item.mealPlanName}, {item.name}, {item.count});
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


    [HttpPost("recipe")]
    public IActionResult InsertMealPlanRecipe([FromBody] MealPlanRecipe item)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }
      try
      {
        _context.Database.ExecuteSqlInterpolated(
             $@"INSERT INTO MealPlanRecipesView(meal_plan_name, name, count)
                    VALUES 
                        ({item.mealPlanName}, {item.name}, {item.count});
                    ");
      }
      catch (Exception ex)
      {
        _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return NoContent();
    }

    [HttpPut("recipe/{id}")]
    public IActionResult UpdateMealPlanRecipe(int id, [FromBody] MealPlanRecipe item)
    {
      if (!ModelState.IsValid || item == null || item.id != id)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
        return BadRequest();
      }

      var mealPlan = _context.MealPlanRecipes.FirstOrDefault(t => t.id == id);
      if (mealPlan == null)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"UPDATE(id) NOT FOUND");
        return NotFound();
      }


      try
      {
        _context.Database.ExecuteSqlInterpolated(
            $@"UPDATE MealPlanRecipesView SET
                        meal_plan_name = {item.mealPlanName},
                        name = {item.name},
                        count = {item.count}
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

    [HttpDelete("recipe/{id}")]
    public IActionResult DeleteMealPlanRecipe(int id)
    {
      var mealPlanRecipe = _context.MealPlanRecipes.FirstOrDefault(t => t.id == id);
      if (mealPlanRecipe == null)
      {
        _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
        return NotFound();
      }

      try
      {
        _context.Database.ExecuteSqlInterpolated($@"DELETE FROM MealPlanRecipesView WHERE id = {mealPlanRecipe.id};");
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
