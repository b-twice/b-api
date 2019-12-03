using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class Books
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long BookAuthorId { get; set; }
        public long BookCategoryId { get; set; }
        public long BookStatusId { get; set; }
        public string ReadYear { get; set; }

        public virtual BookAuthors BookAuthor { get; set; }
        public virtual BookCategories BookCategory { get; set; }
        public virtual BookStatuses BookStatus { get; set; }
    }
}
