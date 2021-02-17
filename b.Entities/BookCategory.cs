using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace b.Entities
{
    public partial class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
