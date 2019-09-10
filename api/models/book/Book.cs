using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Budget.API.Models.Book
{
    [Table("Books")]
    public class Book
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [Column("read_year")]
        public string readYear { get; set; }


        [Required]
        public BookCategory bookCategory { get; set; }

        [Required]
        public BookAuthor bookAuthor { get; set; }

        [Required]
        public BookStatus bookStatus { get; set; }
   }
}




