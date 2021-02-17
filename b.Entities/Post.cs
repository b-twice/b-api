using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class Post
    {
        public long Id { get; set; }
        public long PostGroupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }
        public long Authenticate { get; set; }
        public long Star { get; set; }

        public virtual PostGroup PostGroup { get; set; }
    }
}
