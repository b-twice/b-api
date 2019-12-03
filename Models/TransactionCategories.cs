using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionCategories
    {
        public TransactionCategories()
        {
            Transactions = new HashSet<Transactions>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
