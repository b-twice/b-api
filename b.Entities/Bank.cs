using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class Bank
    {
        public Bank()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
