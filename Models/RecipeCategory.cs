using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("RecipeCategory")]
    public partial class RecipeCategory : AppLookup
    {
        public RecipeCategory()
        {
            Recipes = new HashSet<Recipe>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(Recipe.RecipeCategory))]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
