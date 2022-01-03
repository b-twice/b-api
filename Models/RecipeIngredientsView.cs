using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class RecipeIngredientsView
    {
        public long? Id { get; set; }
        public string Recipe { get; set; }
        public long? RecipeId { get; set; }
        public string Name { get; set; }
        public decimal? Count { get; set; }
        public decimal? Weight { get; set; }
        public string Unit { get; set; }
        public string Measurement { get; set; }
        public string QuantityType { get; set; }
    }
}
