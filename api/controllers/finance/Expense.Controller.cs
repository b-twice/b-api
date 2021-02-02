using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Models.Finance;
using B.API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace B.API 
{

    [Authorize]
    [Route("finance/expenses")]
    public class ExpenseController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public ExpenseController (DatabaseContext context)
        {
            _context = context;
        } 
        [HttpGet()]
        public IActionResult GetExpenses()
        {
            return Ok(_context.Expenses.ToList());
        }

        [HttpGet("year/{year}")]
        public IActionResult GetExpenses(string year, [FromQuery]List<string> monthNames)
        {
            return Ok(
                _context.Expenses
                    .Where(
                        t => 
                            t.year == year 
                            && (monthNames.Count > 0 ?  monthNames.Exists(c => t.month == c) : true)
                    )
        );
        }

        [HttpGet("year/{year}/user/{name}")]
        public IActionResult GetExpenses(string name, string year)
        {
            if (name == "All")
            {

                return Ok(
                    _context.Expenses
                        .Where(
                            t => 
                                t.year == year 
                        ).AsEnumerable()
                        .GroupBy(
                            t => new { t.year, t.categoryName},
                            (key, group) => new Expense {
                                userName = "All",
                                year = key.year,
                                month = null,
                                categoryName = key.categoryName,
                                plannedExpense = group.Sum(o => o.plannedExpense),
                                actualExpense = group.Sum(o => o.actualExpense)
                            }
                        )
 
                );
            }
            return Ok(
                _context.Expenses
                    .Where(
                        t => 
                            t.year == year 
                            && t.userName == name 
                    ).AsEnumerable()
                    .GroupBy(
                        t => new { t.userName, t.year, t.categoryName},
                        (key, group) => new Expense {
                            userName = key.userName,
                            year = key.year,
                            month = null,
                            categoryName = key.categoryName,
                            plannedExpense = group.Sum(o => o.plannedExpense),
                            actualExpense = group.Sum(o => o.actualExpense)
                        }
                    )
            );
        }


        [HttpGet("year/{year}/user/{name}/monthly")]
        public IActionResult GetUserMonthlyExpenses(string name, string year, [FromQuery]List<string> monthNames)
        {
            if (name == "All")
            {

                return Ok(
                    _context.Expenses
                        .Where(
                            t => 
                                t.year == year 
                                && (monthNames.Count > 0 ?  monthNames.Exists(c => t.month == c) : true)
                        ).AsEnumerable()
                        .GroupBy(
                            t => new { t.year, t.month, t.categoryName},
                            (key, group) => new Expense {
                                userName = "All",
                                year = key.year,
                                month = key.month, 
                                categoryName = key.categoryName,
                                plannedExpense = group.Sum(o => o.plannedExpense),
                                actualExpense = group.Sum(o => o.actualExpense)
                            }
                        )
 
                );
            }
            return Ok(
                _context.Expenses
                    .Where(
                        t => 
                            t.year == year 
                            && t.userName == name 
                            &&  (monthNames.Count > 0 ?  monthNames.Exists(c => t.month == c) : true)
                    )
           );
        }

        [HttpGet("year/{year}/user/{name}/category/{category}")]
        public IActionResult GetUserYearlyExpenses(string name, string year, string category)
        {
            return Ok(
                _context.Expenses
                    .Where(
                        t => 
                            t.year == year
                            && t.userName == name
                            && t.categoryName == category
                    )
            );
        }


    }
}
