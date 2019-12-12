using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class YearlyPlannedExpense
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long CategoryId { get; set; }
        public long? Amount { get; set; }

        public virtual TransactionCategory Category { get; set; }
    }
}
