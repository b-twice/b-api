using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Earning
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long UserId { get; set; }
        public long Gross { get; set; }
        public long Taxable { get; set; }
        public long Taxed { get; set; }

        public virtual User User { get; set; }
    }
}
