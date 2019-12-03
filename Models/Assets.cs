using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Assets
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long? Saving { get; set; }
        public long? Hsa { get; set; }
        public long? Retirement { get; set; }
        public long? Stock { get; set; }
        public long? Home { get; set; }
        public long? Auto { get; set; }
    }
}
