using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class FoodUnit
    {
        public FoodUnit()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
