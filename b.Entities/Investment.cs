using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class Investment
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public decimal Saving { get; set; }
        public decimal Hsa { get; set; }
        public decimal Ira { get; set; }
        public decimal Roth { get; set; }
        public decimal Stock { get; set; }
    }
}
