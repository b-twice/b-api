using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class TransactionRecordTag
    {
        public long Id { get; set; }
        public long TransactionRecordId { get; set; }
        public long TagId { get; set; }

        public virtual TransactionTag Tag { get; set; }

        public virtual TransactionRecord TransactionRecord { get; set; }
    }
}
