using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class Asset
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long Saving { get; set; }
        public long Hsa { get; set; }
        public long Retirement { get; set; }
        public long Stock { get; set; }
        public long Home { get; set; }
        public long Auto { get; set; }
    }
}
