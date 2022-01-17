using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Recipe")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Recipe
    {
        public Recipe()
        {
            MealPlanRecipes = new HashSet<MealPlanRecipe>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeNotes = new HashSet<RecipeNote>();
        }

        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RecipeCategoryId { get; set; }
        public long CookbookId { get; set; }
        [Required]
        public string Name { get; set; }
        public long Servings { get; set; }
        public long? PageNumber { get; set; }
        public string Url { get; set; }
        public long? Rating { get; set; }
        public long? Complexity { get; set; }
        public string LastMade { get; set; }
        public long? MakeCount { get; set; }


        [ForeignKey(nameof(CookbookId))]
        [InverseProperty("Recipes")]
        public virtual Cookbook Cookbook { get; set; }
        [ForeignKey(nameof(RecipeCategoryId))]
        [InverseProperty("Recipes")]
        public virtual RecipeCategory RecipeCategory { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Recipes")]
        public virtual User User { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(MealPlanRecipe.Recipe))]
        public virtual ICollection<MealPlanRecipe> MealPlanRecipes { get; set; }
        [InverseProperty(nameof(RecipeIngredient.Recipe))]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        [InverseProperty(nameof(RecipeNote.Recipe))]
        public virtual ICollection<RecipeNote> RecipeNotes { get; set; }
    }
}
