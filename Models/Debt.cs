using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Debt
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long Home { get; set; }
        public long Auto { get; set; }
    }
}
