using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class MealPlanRecipe
    {
        public long Id { get; set; }
        public long MealPlanId { get; set; }
        public long RecipeId { get; set; }
        public long Count { get; set; }

        public virtual MealPlan MealPlan { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
