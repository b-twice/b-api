using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("TransactionCategory")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class TransactionCategory : AppLookup
    {
        public TransactionCategory()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
            YearlyPlannedExpenses = new HashSet<YearlyPlannedExpense>();
        }


        [JsonIgnore]
        [InverseProperty(nameof(TransactionRecord.Category))]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
        [InverseProperty(nameof(YearlyPlannedExpense.Category))]
        public virtual ICollection<YearlyPlannedExpense> YearlyPlannedExpenses { get; set; }
    }
}
