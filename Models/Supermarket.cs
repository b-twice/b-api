using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Supermarket
    {
        public Supermarket()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
