using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("FoodCategory")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class FoodCategory : AppLookup
    {
        public FoodCategory()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(FoodProduct.FoodCategory))]
        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
