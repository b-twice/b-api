using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("CookbookAuthor")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class CookbookAuthor : AppLookup
    {
        public CookbookAuthor()
        {
            Cookbooks = new HashSet<Cookbook>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(Cookbook.CookbookAuthor))]
        public virtual ICollection<Cookbook> Cookbooks { get; set; }
    }
}
