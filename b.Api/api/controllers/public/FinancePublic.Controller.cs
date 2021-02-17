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
  [Route("public/finance/")]
  public class FinancePublicController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public FinancePublicController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet("transactions-per-day")]
    public IActionResult GetTransactionsPerDay()
    {
      return Ok(_context.TransactionsPerDay.ToList().OrderBy(c => c.date));
    }
  }
}