using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class MealPlanRecipesView
    {
        public long? Id { get; set; }
        public long? RecipeId { get; set; }
        public string MealPlanName { get; set; }
        public long? MealPlanId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Cookbook { get; set; }
        public long? PageNumber { get; set; }
        public string Url { get; set; }
        public long? Count { get; set; }
        public byte[] Servings { get; set; }
    }
}
