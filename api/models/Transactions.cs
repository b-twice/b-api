using System;
using System.Collections.Generic;

namespace B.api.models
{
    public partial class Transactions
    {
        public long TransactionId { get; set; }
        public long BankId { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public long? Amount { get; set; }
        public long? Wedding { get; set; }

        public virtual Banks Bank { get; set; }
        public virtual TransactionCategories Category { get; set; }
        public virtual Users User { get; set; }
    }
}
