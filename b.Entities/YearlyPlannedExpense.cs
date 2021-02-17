using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class YearlyPlannedExpense
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public long CategoryId { get; set; }
        public long Amount { get; set; }

        public virtual TransactionCategory Category { get; set; }
    }
}
