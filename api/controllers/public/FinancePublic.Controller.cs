using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using B.API.Models;
using B.API.Entities;
using B.API.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace B.API 
{
    [Route("public/finance/")]
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