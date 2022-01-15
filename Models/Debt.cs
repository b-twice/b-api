using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Debt")]
    public partial class Debt
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Year { get; set; }
        public long Home { get; set; }
        public long Auto { get; set; }
    }
}
