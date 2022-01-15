using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("TransactionRecordTag")]
    public partial class TransactionRecordTag
    {
        [Key]
        public long Id { get; set; }
        public long TransactionRecordId { get; set; }
        public long TagId { get; set; }

        [ForeignKey(nameof(TagId))]
        [InverseProperty(nameof(TransactionTag.TransactionRecordTags))]
        public virtual TransactionTag Tag { get; set; }
        [ForeignKey(nameof(TransactionRecordId))]
        [JsonIgnore]
        [InverseProperty("TransactionRecordTags")]
        public virtual TransactionRecord TransactionRecord { get; set; }
    }
}
