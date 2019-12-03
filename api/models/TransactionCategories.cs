using System;
using System.Collections.Generic;

namespace B.api.models
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
