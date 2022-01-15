using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Cookbook")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Cookbook : AppLookup
    {
        public Cookbook()
        {
            Recipes = new HashSet<Recipe>();
        }

        public long CookbookAuthorId { get; set; }

        [ForeignKey(nameof(CookbookAuthorId))]
        [InverseProperty("Cookbooks")]
        public virtual CookbookAuthor CookbookAuthor { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(Recipe.Cookbook))]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
