using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionCategory
    {
        public TransactionCategory()
        {
            TransactionRecord = new HashSet<TransactionRecord>();
            YearlyPlannedExpense = new HashSet<YearlyPlannedExpense>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransactionRecord> TransactionRecord { get; set; }
        public virtual ICollection<YearlyPlannedExpense> YearlyPlannedExpense { get; set; }
    }
}
