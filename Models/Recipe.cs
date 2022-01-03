using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            MealPlanRecipes = new HashSet<MealPlanRecipe>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public long RecipeCategoryId { get; set; }
        public long CookbookId { get; set; }
        public string Name { get; set; }
        public long Servings { get; set; }
        public long? PageNumber { get; set; }
        public string Url { get; set; }

        public virtual Cookbook Cookbook { get; set; }
        public virtual RecipeCategory RecipeCategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MealPlanRecipe> MealPlanRecipes { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
