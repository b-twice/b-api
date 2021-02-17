using System;
using System.Collections.Generic;

namespace b.Entities
{
  public partial class ExpenseSummary
  {
    public virtual IEnumerable<Expense> expenses { get; set; }

    public decimal PlannedAmount
    {
      get
      {
        decimal total = 0M;
        foreach (var expense in expenses)
        {
          total += expense.PlannedAmount;
        };
        return total;
      }
    }
    public decimal TotalActualAmount
    {
      get
      {
        decimal total = 0M;
        foreach (var expense in expenses)
        {
          total += expense.ActualAmount;
        };
        return total;
      }
    }

    public decimal Remainder
    {
      get
      {
        decimal total = 0M;
        foreach (var expense in expenses)
        {
          total += expense.Remainder;
        };
        return total;
      }
    }

  }
}
