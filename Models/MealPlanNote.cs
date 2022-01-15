using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("MealPlanNote")]
    public partial class MealPlanNote
    {
        [Key]
        public long Id { get; set; }
        public long MealPlanId { get; set; }
        [Required]
        public string Content { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(MealPlanId))]
        [InverseProperty("MealPlanNotes")]
        public virtual MealPlan MealPlan { get; set; }
    }
}
