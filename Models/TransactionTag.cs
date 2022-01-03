using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionTag
    {
        public TransactionTag()
        {
            TransactionRecordTags = new HashSet<TransactionRecordTag>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransactionRecordTag> TransactionRecordTags { get; set; }
    }
}
