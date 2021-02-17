using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class PostGroup
    {
        public PostGroup()
        {
            Posts = new HashSet<Post>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
