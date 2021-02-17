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
  [Route("finance/")]
  public class FinanceDeprecatedController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public FinanceDeprecatedController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet("transaction-categories")]
    public IActionResult GetTransactionCategories()
    {
      return Ok(_context.TransactionCategories.ToList().OrderBy(c => c.name));
    }

    [HttpGet("expense-months")]
    public IActionResult GetExpenseMonths()
    {
      return Ok(_context.ExpenseMonths.ToList());
    }

    [HttpGet("user-profiles/{name}")]
    public IActionResult GetUserProfile(string name)
    {
      return Ok(_context.UserProfiles.First(t => t.name == name));
    }

    [HttpGet("user-summaries/year/{year}/user/{name}")]
    public IActionResult GetUserSummary(string name, int year)
    {
      return Ok(_context.UserSummaries.Where(t => t.userName == name && t.fiscalYear <= year));
    }

    [HttpGet("user-categories-growth/year/{year}/user/{name}")]
    public IActionResult GetUserCategoriesGrowth(string name, string year)
    {
      return Ok(_context.UserCategoriesGrowth.Where(t => t.fiscalYear == year && t.userName == name));
    }
  }
}