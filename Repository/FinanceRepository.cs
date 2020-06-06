using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;
using AutoMapper;

namespace B.API.Database
{

  public class FinanceRepository
  {
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    public FinanceRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public FinancialSummary GetSummary(string year) 
    {
        var asset = _context.Asset.First(o => o.Year.Substring(0,4) == year);
        var debt = _context.Debt.First(o => o.Year.Substring(0,4) == year);
        var investment = _context.Investment.First(o => o.Year.Substring(0,4) == year);
        var earnings = _context.Earning.Where(o => o.Year.Substring(0,4) == year).AsNoTracking();

        var summary = _mapper.Map<FinancialSummary>(asset);
        summary = _mapper.Map(debt, summary);
        summary = _mapper.Map(investment, summary);
        summary = _mapper.Map(earnings, summary);
        return summary;
    }



    public IQueryable<TransactionTotal> FindTransactionCategoryTotals(string year) 
    {
        return (
            from tr in _context.TransactionRecord 
            where tr.Date.Substring(0,4) == year
            group tr by new { Id = tr.Category.Id, Name = tr.Category.Name } into g
            orderby g.Key.Name
            select new TransactionTotal {
              Id = g.Key.Id,
              Name = g.Key.Name,
              Amount = g.Sum(gc => gc.Amount)
            }
        ).AsNoTracking();
    }

    public IQueryable<TransactionTotal> FindTransactionCategoryMonthlyTotals(string year, string month) 
    {
        return (
            from tr in _context.TransactionRecord 
            where tr.Date.Substring(0,4) == year && tr.Date.Substring(5, 2) == month
            group tr by new { Id = tr.Category.Id, Name = tr.Category.Name } into g
            orderby g.Key.Name
            select new TransactionTotal {
              Id = g.Key.Id,
              Name = g.Key.Name,
              Amount = g.Sum(gc => gc.Amount)
            }
        ).AsNoTracking();
    }

    public IQueryable<TransactionTotal> FindTransactionMonthlyTotals(string year) 
    {
        return (
            from tr in _context.TransactionRecord 
            where tr.Date.Substring(0,4) == year
            group tr by new { Name = tr.Date.Substring(5,2)} into g
            orderby  g.Key.Name descending
            select new TransactionTotal {
              Id = 0,
              Name = g.Key.Name,
              Amount = g.Sum(gc => gc.Amount)
            }
        ).AsNoTracking();
    }



    public IEnumerable<TransactionTotal> FindTransactionCategoryTagTotals(string year, string categoryName) 
    {
        var records = _context.TransactionRecord
          .Include(t => t.Category)
          .Where(record => record.Date.Substring(0,4) == year && record.Category.Name == categoryName).AsNoTracking().AsEnumerable();
        
        return (
            from tr in records
            join tags in _context.TransactionRecordTag.AsEnumerable() on tr.Id equals tags.TransactionRecordId into trt
            from tags in trt.DefaultIfEmpty()
            join tag in _context.TransactionTag on tags?.TagId equals tag.Id into t
            from tag in t.DefaultIfEmpty()
            group tr by new { Id = tags == null ? 0 : tag.Id, Name = tags == null ? "Untagged" : tag.Name} into g
            orderby g.Key.Name
            select new TransactionTotal {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Amount = g.Sum(gc => gc.Amount)
            }
        );
    }



    public IQueryable<Expense> FindYearlyExpenses(List<string> years, List<long> categories) 
    {
      var transactionGroups = (
        from tr in _context.TransactionRecord 
        where 
          years.Contains(tr.Date.Substring(0,4))
        group tr by new { Year = tr.Date.Substring(0,4), Name = tr.Category.Name } into g
        select new {
            Name = g.Key.Name,
            Year = g.Key.Year,
            Amount = g.Sum(gc => gc.Amount)
        }
      ).AsNoTracking();
      var plannedExpenses = (
        from e in _context.YearlyPlannedExpense
        where 
          years.Contains(e.Date.Substring(0,4))
          && (categories.Count > 0 ?  categories.Contains(e.Category.Id) : true)
        group e by new { Year = e.Date.Substring(0,4), Category = e.Category.Name} into eg
        select new {
            Name  = eg.Key.Category,
            Year = eg.Key.Year,
            PlannedAmount = eg.Sum(gc => gc.Amount)
        }
      ).AsNoTracking();
      var expenses = (
        from e in plannedExpenses
        join g in transactionGroups on new { A = e.Year, B = e.Name} equals new { A = g.Year, B = g.Name} into gj
        from subTransactionGroup in gj.DefaultIfEmpty()
        select new Expense {
            Date = e.Year,
            CategoryName = e.Name,
            PlannedAmount = e.PlannedAmount,
            ActualAmount = subTransactionGroup.Amount
        }
      );
      return expenses.OrderBy(o => o.CategoryName);
    }



    public IQueryable<Expense> FindMonthlyExpenses(List<string> years, List<string> months, List<long> categories) 
    {
      var yearMonths = new List<string>();
      foreach(var year in years) {
        foreach(var month in months) {
          yearMonths.Add($"{year}-{month}");
        }
      }
      var transactionGroups = (
          from tr in _context.TransactionRecord 
          where 
            yearMonths.Contains(tr.Date.Substring(0,7))
          group tr by new { YearMonth = tr.Date.Substring(0,7), Name = tr.Category.Name } into g
          select new {
              Name = g.Key.Name,
              YearMonth = g.Key.YearMonth,
              Amount = g.Sum(gc => gc.Amount)
          }
      );
      var expenses = (
          from e in _context.YearlyPlannedExpense
          where 
            yearMonths.Contains(e.Date.Substring(0,7))
            && (categories.Count > 0 ?  categories.Contains(e.Category.Id) : true)
          join g in transactionGroups on new { A = e.Date.Substring(0,7), B = e.Category.Name} equals new { A = g.YearMonth, B = g.Name} into gj
          from subTransactionGroup in gj.DefaultIfEmpty()
          select new Expense {
              Date = e.Date.Substring(0,7),
              CategoryName = e.Category.Name,
              PlannedAmount = e.Amount,
              ActualAmount = subTransactionGroup.Amount
          }
      );
      return expenses.OrderByDescending(o => o.Date).ThenBy(o => o.CategoryName);
    }

  }
}
