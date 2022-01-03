using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionCategory
    {
        public TransactionCategory()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
            YearlyPlannedExpenses = new HashSet<YearlyPlannedExpense>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
        public virtual ICollection<YearlyPlannedExpense> YearlyPlannedExpenses { get; set; }
    }
}
