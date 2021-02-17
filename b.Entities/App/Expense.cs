using System;
using System.Collections.Generic;

namespace b.Entities
{
  public partial class Expense
  {
    public string Date { get; set; }
    public virtual TransactionCategory Category { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal ActualAmount { get; set; }

    public decimal Remainder
    {
      get
      {
        return PlannedAmount - ActualAmount;
      }
    }

  }
}
