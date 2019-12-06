using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionRecord
    {
        public long TransactionRecordId { get; set; }
        public long BankId { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public long? Amount { get; set; }
        public long? Wedding { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual TransactionCategory Category { get; set; }
        public virtual Users User { get; set; }
    }
}
