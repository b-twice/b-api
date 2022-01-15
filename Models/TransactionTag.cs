using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("TransactionTag")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class TransactionTag : AppLookup
    {
        public TransactionTag()
        {
            TransactionRecordTags = new HashSet<TransactionRecordTag>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(TransactionRecordTag.Tag))]
        public virtual ICollection<TransactionRecordTag> TransactionRecordTags { get; set; }
    }
}
