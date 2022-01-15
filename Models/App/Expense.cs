using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Expense
    {
        public string Date { get; set; }
        public virtual TransactionCategory Category { get; set; }
        public long PlannedAmount { get; set; }
        public long ActualAmount { get; set; }

        public string CategoryName { get; set; }
        public long Remainder { 
            get {
                return  PlannedAmount - ActualAmount;
            } 
        }

    }
}
