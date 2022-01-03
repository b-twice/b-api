using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class MealPlan
    {
        public MealPlan()
        {
            MealPlanRecipes = new HashSet<MealPlanRecipe>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Days { get; set; }
        public string Notes { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MealPlanRecipe> MealPlanRecipes { get; set; }
    }
}
