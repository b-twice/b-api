using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
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

        [JsonIgnore]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
        [JsonIgnore]
        public virtual ICollection<YearlyPlannedExpense> YearlyPlannedExpenses { get; set; }
    }
}
