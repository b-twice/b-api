using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("FoodProduct")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class FoodProduct
    {
        public FoodProduct()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        [Key]
        public long Id { get; set; }
        public long FoodCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public long FoodUnitId { get; set; }
        public long Dirty { get; set; }
        public long? SupermarketId { get; set; }
        public string Measurement { get; set; }
        [Column(TypeName = "INTERGER")]
        public long FoodQuantityTypeId { get; set; }

        [ForeignKey(nameof(FoodCategoryId))]
        [InverseProperty("FoodProducts")]
        public virtual FoodCategory FoodCategory { get; set; }
        [ForeignKey(nameof(FoodQuantityTypeId))]
        [InverseProperty("FoodProducts")]
        public virtual FoodQuantityType FoodQuantityType { get; set; }
        [ForeignKey(nameof(FoodUnitId))]
        [InverseProperty("FoodProducts")]
        public virtual FoodUnit FoodUnit { get; set; }
        [ForeignKey(nameof(SupermarketId))]
        [InverseProperty("FoodProducts")]
        public virtual Supermarket Supermarket { get; set; }
        [InverseProperty(nameof(RecipeIngredient.FoodProduct))]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
