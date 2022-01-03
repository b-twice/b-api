using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class CookbookAuthor
    {
        public CookbookAuthor()
        {
            Cookbooks = new HashSet<Cookbook>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cookbook> Cookbooks { get; set; }
    }
}
