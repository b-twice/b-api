using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookStatus
    {
        public BookStatus()
        {
            Book = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
