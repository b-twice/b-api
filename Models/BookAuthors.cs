using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookAuthors
    {
        public BookAuthors()
        {
            Books = new HashSet<Books>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
