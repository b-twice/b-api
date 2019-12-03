using System;
using System.Collections.Generic;

namespace B.api.models
{
    public partial class Earnings
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long UserId { get; set; }
        public long? Gross { get; set; }
        public long? Taxable { get; set; }
        public long? Taxed { get; set; }

        public virtual Users User { get; set; }
    }
}
