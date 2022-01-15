using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("RecipeIngredient")]
    public partial class RecipeIngredient
    {
        [Key]
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public long FoodProductId { get; set; }
        public decimal? Count { get; set; }
        public decimal? Weight { get; set; }
        [Required]
        public string Measurement { get; set; }

        [ForeignKey(nameof(FoodProductId))]
        [InverseProperty("RecipeIngredients")]
        public virtual FoodProduct FoodProduct { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(RecipeId))]
        [InverseProperty("RecipeIngredients")]
        public virtual Recipe Recipe { get; set; }
    }
}
