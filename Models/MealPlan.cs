using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class MealPlan
    {
        public MealPlan()
        {
            MealPlanNotes = new HashSet<MealPlanNote>();
            MealPlanRecipes = new HashSet<MealPlanRecipe>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MealPlanNote> MealPlanNotes { get; set; }
        public virtual ICollection<MealPlanRecipe> MealPlanRecipes { get; set; }
    }
}
