using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Bank")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Bank : AppLookup
    {
        public Bank()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(TransactionRecord.Bank))]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
