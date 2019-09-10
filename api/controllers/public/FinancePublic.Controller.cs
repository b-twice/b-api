using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Models;
using Budget.API.Entities;
using Budget.API.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace Budget.API 
{
    [Route("api/public/finance/")]
    public class FinancePublicController: ControllerBase
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