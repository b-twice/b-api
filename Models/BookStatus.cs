using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("BookStatus")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class BookStatus :  AppKeywordLookup
    {
        public BookStatus()
        {
            Books = new HashSet<Book>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(Book.BookStatus))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
