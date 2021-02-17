using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class Earning
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long Gross { get; set; }
        public long Taxable { get; set; }
        public long Taxed { get; set; }
    }
}
