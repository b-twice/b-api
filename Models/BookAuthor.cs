using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookAuthor
    {
        public BookAuthor()
        {
            Books = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
