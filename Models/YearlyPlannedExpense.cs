using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("YearlyPlannedExpense")]
    [Index(nameof(Date), nameof(CategoryId), IsUnique = true)]
    public partial class YearlyPlannedExpense
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Date { get; set; }
        public long CategoryId { get; set; }
        public long Amount { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(TransactionCategory.YearlyPlannedExpenses))]
        public virtual TransactionCategory Category { get; set; }
    }
}
