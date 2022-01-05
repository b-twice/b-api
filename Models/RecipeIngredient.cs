using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class RecipeIngredient
    {
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public long FoodProductId { get; set; }
        public decimal? Count { get; set; }
        public decimal? Weight { get; set; }
        public string Measurement { get; set; }

        public virtual FoodProduct FoodProduct { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
