using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class BookStatuses
    {
        public BookStatuses()
        {
            Books = new HashSet<Books>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
