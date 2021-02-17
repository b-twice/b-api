using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class TransactionTag
    {
        public TransactionTag()
        {
            TransactionRecordTags = new HashSet<TransactionRecordTag>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransactionRecordTag> TransactionRecordTags { get; set; }
    }
}
