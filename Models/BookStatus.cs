using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookStatus
    {
        public BookStatus()
        {
            Books = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
