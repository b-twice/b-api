using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class TransactionsStaging
    {
        public string Bank { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public long? Amount { get; set; }
        public long? Automap { get; set; }
        public long? Wedding { get; set; }
    }
}
