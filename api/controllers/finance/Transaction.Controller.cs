using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using B.API.Models.Finance;
using B.API.Entities;
using B.API.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace B.API 
{
    [Authorize]
    [Route("api/finance/transactions")]
    public class TransactionDeprecatedController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public TransactionDeprecatedController (DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("year/{year}/user/{name}")]
        public IActionResult GetTransactions(string name, string year, [FromQuery]List<string> categoryNames)
        {
            bool isAll = name == "All" ? true : false;
            return Ok(
                _context.Transactions.Where(
                    t => 
                        DateHelper.ParseYear(t.date) == year
                        && (isAll || t.userName == name)
                        && (categoryNames.Count > 0 ?  categoryNames.Exists(c => t.categoryName == c) : true)
                )
            );
        }

        [HttpGet("year/{year}/user/{name}/monthly/range/{range}")]
        public IActionResult GetTransactionsMonthly(string name, string year, int range, [FromQuery]List<string> categoryNames)
        {
            bool isAll = name == "All" ? true : false;
            int yearParse;
            int.TryParse(year, out yearParse);
            yearParse -= range;
            return Ok(
                _context.TransactionsMonthly.Where(
                    t => 
                        t.year >= yearParse 
                        && (isAll || t.user == name)
                        && (categoryNames.Count > 0 ?  categoryNames.Exists(c => t.category == c) : true)
                ).AsEnumerable()  // convert to enumerable otherwise throwing reduce node error
                .GroupBy(
                    t => new { t.year, t.month},
                    (key, group) => new TransactionMonthly{
                        year = key.year,
                        month = key.month, 
                        amount = group.Sum(o => o.amount)
                    }
                )
               .OrderBy(d => d.month)
            );

        }
       
        [HttpGet("year/{year}/month/{month}/user/{name}/category/{category}")]
        public IActionResult GetTransactions(string name, string year, string month, string category)
        {
            bool isAll = name == "All" ? true : false;
            return Ok(_context.Transactions.Where(
                t => 
                    DateHelper.ParseYear(t.date) == year
                    && DateHelper.ParseMonth(t.date) == month
                    && (isAll || t.userName == name)
                    && t.categoryName == category
                )
            );
        }

    }

}
