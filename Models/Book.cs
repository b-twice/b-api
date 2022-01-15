using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Book")]
    [Index(nameof(Name), nameof(BookAuthorId), nameof(BookCategoryId), nameof(BookStatusId), nameof(ReadDate), IsUnique = true)]
    public partial class Book
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public long BookAuthorId { get; set; }
        public long BookCategoryId { get; set; }
        public long BookStatusId { get; set; }
        [Required]
        public string ReadDate { get; set; }

        [ForeignKey(nameof(BookAuthorId))]
        [InverseProperty("Books")]
        public virtual BookAuthor BookAuthor { get; set; }
        [ForeignKey(nameof(BookCategoryId))]
        [InverseProperty("Books")]
        public virtual BookCategory BookCategory { get; set; }
        [ForeignKey(nameof(BookStatusId))]
        [InverseProperty("Books")]
        public virtual BookStatus BookStatus { get; set; }
    }
}
