using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using b.Entities;
using b.Api.Entities;
using b.Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace b.Api
{

  [Authorize]
  [Route("food/products")]
  public class FoodProductController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public FoodProductController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet()]
    public IActionResult GetFoodProducts()
    {
      return Ok(_context.FoodProducts.OrderBy(o => o.name).ToList());
    }

    [HttpGet("product/{name}")]
    public IActionResult GetFoodProducts(string name)
    {
      return Ok(_context.FoodProducts.FirstOrDefault(o => o.name == name));
    }


    [HttpGet("year/{year}")]
    public IActionResult GetAnnualFoodProducts(string year, [FromQuery] List<string> categoryNames)
    {
      return Ok(_context.AnnualFoodProducts.Where(g => g.year == year && (categoryNames.Count > 0 ? categoryNames.Exists(c => g.category == c) : true)));
    }

    [HttpGet("year/{year}/user/{name}")]
    public IActionResult GetAnnualFoodProducts(string name, string year, [FromQuery] List<string> categoryNames)
    {
      if (name == "All")
      {

        return Ok(
            _context.AnnualFoodProducts
                .Where(g => g.year == year && (categoryNames.Count > 0 ? categoryNames.Exists(c => g.category == c) : true)).AsEnumerable()
                .GroupBy(
                    g => new { g.category, g.name, g.year, g.unit },
                    (key, group) => new
                    {
                      user = "All",
                      category = key.category,
                      name = key.name,
                      year = key.year,
                      count = group.Sum(o => o.count),
                      weight = group.Sum(o => o.weight),
                      unit = key.unit,
                      amount = group.Sum(o => o.amount),
                      unitPrice = group.Average(o => o.unitPrice)
                    }
                )
        );
      }
      return Ok(_context.AnnualFoodProducts.Where(g => g.year == year && g.user == name && (categoryNames.Count > 0 ? categoryNames.Exists(c => g.category == c) : true)));
    }


    [HttpGet("year/{year}/user/{name}/category/{category}")]
    public IActionResult GetAnnualFoodProducts(string name, string year, string category)
    {
      if (name == "All")
      {

        return Ok(
            _context.AnnualFoodProducts.Where(g => g.year == year && g.category == category).AsEnumerable()
                .GroupBy(
                    g => new { g.category, g.name, g.year, g.unit },
                    (key, group) => new
                    {
                      user = "All",
                      category = key.category,
                      name = key.name,
                      year = key.year,
                      count = group.Sum(o => o.count),
                      weight = group.Sum(o => o.weight),
                      unit = key.unit,
                      amount = group.Sum(o => o.amount),
                      unitPrice = group.Average(o => o.unitPrice)
                    }
                )
        );
      }
      return Ok(_context.AnnualFoodProducts.Where(g => g.year == year && g.user == name && g.category == category));
    }

    [HttpGet("year/{year}/user/{name}/food-product/{foodProductName}")]
    public IActionResult GetAnnualFoodProductsByName(string name, string year, string foodProductName)
    {
      if (name == "All")
      {

        return Ok(_context.AnnualFoodProducts.Where(g => g.year == year && g.name == foodProductName).AsEnumerable()
                .GroupBy(
                    g => new { g.category, g.name, g.year, g.unit },
                    (key, group) => new
                    {
                      user = "All",
                      category = key.category,
                      name = key.name,
                      year = key.year,
                      count = group.Sum(o => o.count),
                      weight = group.Sum(o => o.weight),
                      unit = key.unit,
                      amount = group.Sum(o => o.amount),
                      unitPrice = group.Average(o => o.unitPrice)
                    }
                )
        );
      }
      return Ok(_context.AnnualFoodProducts.Where(g => g.year == year && g.user == name && g.name == foodProductName));
    }
  }
}

