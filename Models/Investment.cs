using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Investment
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public long? Saving { get; set; }
        public long? Hsa { get; set; }
        public long? Ira { get; set; }
        public long? Roth { get; set; }
        public long? Stock { get; set; }
    }
}
