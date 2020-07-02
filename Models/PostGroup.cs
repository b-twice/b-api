using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class PostGroup
    {
        public PostGroup()
        {
            Post = new HashSet<Post>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
