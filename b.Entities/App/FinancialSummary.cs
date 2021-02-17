using System;
using System.Collections.Generic;

namespace b.Entities
{
  public partial class FinancialSummary
  {
    public virtual Asset Asset { get; set; }
    public virtual Investment Investment { get; set; }
    public virtual Debt Debt { get; set; }

    public virtual Earning Earnings { get; set; }

    public decimal NetWorth
    {
      get
      {
        return AssetTotal - DebtTotal;
      }
    }

    public decimal AssetTotal
    {
      get
      {
        return Asset.Auto + Asset.Home + Asset.Hsa + Asset.Retirement + Asset.Saving + Asset.Stock;

      }
    }

    public decimal DebtTotal
    {
      get
      {
        return Debt.Auto + Debt.Home;
      }
    }
  }
}
