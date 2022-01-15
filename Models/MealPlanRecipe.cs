using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("MealPlanRecipe")]
    public partial class MealPlanRecipe
    {
        [Key]
        public long Id { get; set; }
        public long MealPlanId { get; set; }
        public long RecipeId { get; set; }
        public long Count { get; set; }

        [ForeignKey(nameof(MealPlanId))]
        [InverseProperty("MealPlanRecipes")]
        public virtual MealPlan MealPlan { get; set; }
        [ForeignKey(nameof(RecipeId))]
        [InverseProperty("MealPlanRecipes")]
        public virtual Recipe Recipe { get; set; }
    }
}
