using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;
using AutoMapper;

namespace B.API.Database
{

  public class FinanceRepository
  {
    private readonly ApiDbContext _context;

    private readonly IMapper _mapper;

    public FinanceRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public FinancialSummary GetSummary(string year) 
    {
        var asset = _context.Asset.First(o => o.Year.Substring(0,4) == year);
        var debt = _context.Debt.First(o => o.Year.Substring(0,4) == year);
        var investment = _context.Investment.First(o => o.Year.Substring(0,4) == year);
        var earnings = _context.Earning.Where(o => o.Year.Substring(0,4) == year);

        var summary = _mapper.Map<FinancialSummary>(asset);
        summary = _mapper.Map(debt, summary);
        summary = _mapper.Map(investment, summary);
        summary = _mapper.Map(earnings, summary);
        return summary;
    }



    public IQueryable<TransactionCategoryTotal> FindTransactionCategoryTotals(string year) 
    {
        return (
            from tr in _context.TransactionRecord 
            where tr.Date.Substring(0,4) == year
            group tr by tr.Category.Name into g
            orderby g.Key
            select new TransactionCategoryTotal {
                Name = g.Key,
                Amount = g.Sum(gc => gc.Amount)
            }
        );
    }

    public IQueryable<Expense> FindMonthlyExpenses(string year, string month) 
    {
      var transactionGroups = (
          from tr in _context.TransactionRecord 
          where !string.IsNullOrEmpty(month) ? tr.Date.Substring(0,7) == $"{year}-{month}" : tr.Date.Substring(0,4) == $"{year}" 
          group tr by new { YearMonth = tr.Date.Substring(0,7), Name = tr.Category.Name } into g
          select new {
              Name = g.Key.Name,
              YearMonth = g.Key.YearMonth,
              Amount = g.Sum(gc => gc.Amount)
          }
      );
      // if (!string.IsNullOrEmpty(year)) {
      //   if (!string.IsNullOrEmpty(month)) {
      //     var yearMonth = $"{year}-{month}";
      //     transactionGroups.Where(tr => tr.YearMonth == yearMonth);
      //   }
      //   else {
      //     transactionGroups.Where(tr => tr.YearMonth.Substring(0, 4) == year);
      //   }
      // }
      var expenses = (
          from e in _context.YearlyPlannedExpense
          where !string.IsNullOrEmpty(month) ? e.Date.Substring(0,7) == $"{year}-{month}" : e.Date.Substring(0,4) == $"{year}" 
          join g in transactionGroups on new { A = e.Date.Substring(0,7), B = e.Category.Name} equals new { A = g.YearMonth, B = g.Name}
          select new Expense {
              Date = e.Date,
              Category = e.Category,
              PlannedAmount = e.Amount,
              ActualAmount = g.Amount
          }
      );
      return expenses.OrderByDescending(o => o.Date);
    }
  }
}
