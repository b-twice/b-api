using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("TransactionRecord")]
    [Index(nameof(BankId), nameof(UserId), nameof(Date), nameof(Description), nameof(Amount), IsUnique = true)]
    public partial class TransactionRecord
    {
        public TransactionRecord()
        {
            TransactionRecordTags = new HashSet<TransactionRecordTag>();
        }

        [Key]
        public long Id { get; set; }
        public long BankId { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Description { get; set; }
        public long Amount { get; set; }
        public string Note { get; set; }

        [ForeignKey(nameof(BankId))]
        [InverseProperty("TransactionRecords")]
        public virtual Bank Bank { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(TransactionCategory.TransactionRecords))]
        public virtual TransactionCategory Category { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("TransactionRecords")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(TransactionRecordTag.TransactionRecord))]
        public virtual ICollection<TransactionRecordTag> TransactionRecordTags { get; set; }
    }
}
