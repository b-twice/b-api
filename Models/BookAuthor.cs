using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookAuthor
    {
        public BookAuthor()
        {
            Book = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
