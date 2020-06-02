using System;
using System.Collections.Generic;

namespace B.API.Models
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
