using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class Debt
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long Home { get; set; }
        public long Auto { get; set; }
    }
}
