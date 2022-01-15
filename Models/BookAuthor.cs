using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("BookAuthor")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class BookAuthor :  AppLookup
    {
        public BookAuthor()
        {
            Books = new HashSet<Book>();
        }

        [JsonIgnore]
        [InverseProperty(nameof(Book.BookAuthor))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
