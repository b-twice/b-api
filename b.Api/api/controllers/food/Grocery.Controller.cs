using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using b.Entities.Food;
using b.Entities.Finance;
using b.Api.Entities;
using b.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Budget.Core;


namespace b.Api
{


  [Authorize]
  [Route("food/groceries")]
  public class GroceryController : ControllerBase
  {
    private readonly DatabaseContext _context;
    private readonly ILogger _logger;

    public GroceryController(DatabaseContext context, ILogger<GroceryController> logger)
    {
      _context = context;
      _logger = logger;

    }

    [HttpGet("grocery/{id}")]
    public IActionResult GetLatestGrocery(int id)
    {
      return Ok(
          _context.Groceries.Where(
              t =>
                  t.id == id
          ).FirstOrDefault()
     );
    }



    [HttpGet("year/{year}/user/{name}")]
    public IActionResult GetGroceries(string name, string year, [FromQuery] List<string> categoryNames)
    {
      bool isAll = name == "All" ? true : false;
      return Ok(
          _context.Groceries.Where(
              g =>
                  DateHelper.ParseYear(g.date) == year
                  && (isAll || g.user == name)
                  && (categoryNames.Count > 0 ? categoryNames.Exists(c => g.category == c) : true)
          )
      );
    }



    [HttpGet("year/{year}/user/{name}/grocery/{groceryName}")]
    public IActionResult GetGroceriesByName(string name, string year, string groceryName)
    {
      bool isAll = name == "All" ? true : false;
      return Ok(
          _context.Groceries.Where(
              g =>
                  DateHelper.ParseYear(g.date) == year
                  && (isAll || g.user == name)
                  && g.name == groceryName
          )
      );
    }

    [HttpGet("year/{year}/user/{name}/monthly")]
    public IActionResult GetGroceriesMonthly(string name, string year, [FromQuery] List<string> categoryNames)
    {
      bool isAll = name == "All" ? true : false;
      return Ok(
          _context.Groceries.Where(
              g =>
                  DateHelper.ParseYear(g.date) == year
                  && (isAll || g.user == name)
                  && (categoryNames.Count > 0 ? categoryNames.Exists(c => g.category == c) : true)
          ).AsEnumerable()
          .GroupBy(
              g => new
              {
                Month = DateHelper.ParseMonth(g.date)
              },
              (key, group) => new MonthlyExpense
              {
                userName = name,
                year = year,
                month = key.Month,
                amount = group.Sum(o => o.amount),
              }
          )
      );

    }


    [HttpGet("year/{year}/user/{name}/monthly/range/{range}")]
    public IActionResult GetTransactionsMonthly(string name, string year, int range, [FromQuery] List<string> categoryNames)
    {
      bool isAll = name == "All" ? true : false;
      int yearParse;
      int.TryParse(year, out yearParse);
      yearParse -= range;
      return Ok(
          _context.GroceriesMonthly.Where(
              t =>
                  t.year >= yearParse
                  && (isAll || t.user == name)
                  && (categoryNames.Count > 0 ? categoryNames.Exists(c => t.category == c) : true)
          ).AsEnumerable()
          .GroupBy(
              t => new { t.year, t.month },
              (key, group) => new TransactionMonthly
              {
                year = key.year,
                month = key.month,
                amount = group.Sum(o => o.amount)
              }
          )
          .OrderBy(d => d.month)
      );
    }
    [HttpGet("latest/{foodProduct}/supermarket/{supermarket}")]
    public IActionResult GetLatestGrocery(string foodProduct, string supermarket)
    {
      return Ok(
          _context.Groceries.Where(
              t =>
                  t.name == foodProduct
                  && t.supermarket == supermarket
          )
          .OrderByDescending(t => t.date).FirstOrDefault()
     );
    }

    [HttpPost("")]
    public IActionResult CheckoutGroceries([FromBody] GroceryCart cart)
    {
      if (!ModelState.IsValid || cart.basket.Count == 0)
      {
        _logger.LogWarning(LoggingEvents.InsertItemBadRequest, $"Insert BAD REQUEST");
        return BadRequest();
      }

      try
      {

        foreach (Grocery item in cart.basket)
        {
          _context.Database.ExecuteSqlInterpolated(
              $@"INSERT INTO GroceriesView (user, supermarket, name, date, count, weight, organic, seasonal, amount)
                        VALUES 
                            ({item.user}, {item.supermarket}, {item.name}, {item.date}, {item.count}, {item.weight}, {item.organic}, {item.seasonal}, {item.amount});
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

    [HttpPut("grocery/{id}")]
    public IActionResult UpdateGrocery(int id, [FromBody] Grocery item)
    {
      if (!ModelState.IsValid || item == null || item.id != id)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
        return BadRequest("Invalid parameters");
      }

      var grocery = _context.Groceries.FirstOrDefault(t => t.id == id);
      if (grocery == null)
      {
        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"UPDATE(id) NOT FOUND");
        return NotFound();
      }


      try
      {
        _context.Database.ExecuteSqlInterpolated(
            $@"UPDATE GroceriesView SET
                        user = {item.user},
                        supermarket = {item.supermarket},
                        name = {item.name},
                        date = {item.date},
                        count = {item.count},
                        weight = {item.weight},
                        organic = {item.organic},
                        seasonal = {item.seasonal},
                        amount = {item.amount}
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

    [HttpDelete("grocery/{id}")]
    public IActionResult DeleteGrocery(int id)
    {
      var grocery = _context.Groceries.FirstOrDefault(t => t.id == id);
      if (grocery == null)
      {
        _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
        return NotFound();
      }


      try
      {
        _context.Database.ExecuteSqlInterpolated($@"DELETE FROM GroceriesView WHERE id = {grocery.id};");
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
