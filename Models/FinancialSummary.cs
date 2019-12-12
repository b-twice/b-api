using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class FinancialSummary
    {
        public virtual Asset Asset { get; set; }
        public virtual Investment Investment { get; set; }
        public virtual Debt Debt { get; set; }

        public virtual ICollection<Earning> Earnings { get; set;}

        public long totalIncome {
            get {
                long total = 0;
                foreach( var earning in Earnings) {
                    total += earning.Gross;
                }
                return total;
            }
        }

        public long NetWorth { 
            get {
                return AssetTotal - DebtTotal;
            }
        }

        public long AssetTotal {
            get {
                return Asset.Auto + Asset.Home + Asset.Hsa + Asset.Retirement + Asset.Saving + Asset.Stock;

            }
        }

        public long DebtTotal { 
            get {
                return Debt.Auto + Debt.Home;
            }
        }
    }
}
