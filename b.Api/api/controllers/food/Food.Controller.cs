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
  [Route("food")]
  public class FoodController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public FoodController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet("categories")]
    public IActionResult GetFoodCategories()
    {
      return Ok(_context.FoodCategories.ToList().OrderBy(c => c.name));
    }

    [HttpGet("supermarkets")]
    public IActionResult GetSupermarkets()
    {
      return Ok(_context.Supermarkets.OrderBy(o => o.name).ToList());
    }

    [HttpGet("cookbooks")]
    public IActionResult GetCookbooks()
    {
      return Ok(_context.Cookbooks.OrderBy(o => o.title).ToList());
    }
  }
}

