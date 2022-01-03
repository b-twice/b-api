using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class FoodProduct
    {
        public FoodProduct()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public long Id { get; set; }
        public long FoodCategoryId { get; set; }
        public string Name { get; set; }
        public long FoodUnitId { get; set; }
        public long Dirty { get; set; }
        public long? SupermarketId { get; set; }
        public string Measurement { get; set; }
        public long FoodQuantityTypeId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }
        public virtual FoodQuantityType FoodQuantityType { get; set; }
        public virtual FoodUnit FoodUnit { get; set; }
        public virtual Supermarket Supermarket { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
