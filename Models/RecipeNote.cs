using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class RecipeNote
    {
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public string Content { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
