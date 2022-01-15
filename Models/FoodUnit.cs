using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("FoodUnit")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class FoodUnit : AppLookup
    {
        public FoodUnit()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }


        [JsonIgnore]
        [InverseProperty(nameof(FoodProduct.FoodUnit))]
        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
