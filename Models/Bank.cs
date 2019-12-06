using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Bank
    {
        public Bank()
        {
            TransactionRecord = new HashSet<TransactionRecord>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransactionRecord> TransactionRecord { get; set; }
    }
}
