using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long BookAuthorId { get; set; }
        public long BookCategoryId { get; set; }
        public long BookStatusId { get; set; }
        public string ReadDate { get; set; }

        public virtual BookAuthor BookAuthor { get; set; }
        public virtual BookCategory BookCategory { get; set; }
        public virtual BookStatus BookStatus { get; set; }
    }
}
