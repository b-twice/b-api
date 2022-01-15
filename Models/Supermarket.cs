using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Supermarket")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Supermarket : AppLookup
    {
        public Supermarket()
        {
            FoodProducts = new HashSet<FoodProduct>();
        }

        [Required]
        public string Code { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(FoodProduct.Supermarket))]
        public virtual ICollection<FoodProduct> FoodProducts { get; set; }
    }
}
