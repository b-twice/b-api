using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("MealPlan")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class MealPlan
    {
        public MealPlan()
        {
            MealPlanNotes = new HashSet<MealPlanNote>();
            MealPlanRecipes = new HashSet<MealPlanRecipe>();
        }

        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Date { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("MealPlans")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(MealPlanNote.MealPlan))]
        public virtual ICollection<MealPlanNote> MealPlanNotes { get; set; }
        [InverseProperty(nameof(MealPlanRecipe.MealPlan))]
        public virtual ICollection<MealPlanRecipe> MealPlanRecipes { get; set; }
    }
}
