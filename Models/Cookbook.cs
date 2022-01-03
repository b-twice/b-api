using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Cookbook
    {
        public Cookbook()
        {
            Recipes = new HashSet<Recipe>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CookbookAuthorId { get; set; }

        public virtual CookbookAuthor CookbookAuthor { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
