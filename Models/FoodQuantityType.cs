using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class FoodQuantityType
    {
        public FoodQuantityType()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
