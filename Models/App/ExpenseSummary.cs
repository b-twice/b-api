using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class ExpenseSummary
    {
        public virtual IEnumerable<Expense> expenses { get; set; }

        public long PlannedAmount { 
          get {
            long total = 0;
            foreach( var expense in expenses) {
              total += expense.PlannedAmount;
            };
            return total;
          } 
        }
        public long TotalActualAmount {
          get {
            long total = 0;
            foreach( var expense in expenses) {
              total += expense.ActualAmount;
            };
            return total;
          } 
        }

        public long Remainder { 
            get {
              long total = 0;
              foreach( var expense in expenses) {
                total += expense.Remainder;
              };
              return total;
            } 
        }

    }
}
