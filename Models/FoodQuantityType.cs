using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("FoodQuantityType")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class FoodQuantityType : AppLookup
    {
        public FoodQuantityType()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(FoodProduct.FoodQuantityType))]
        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
