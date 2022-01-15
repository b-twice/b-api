using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("BookCategory")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class BookCategory :  AppLookup
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }


        [JsonIgnore]
        [InverseProperty(nameof(Book.BookCategory))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
