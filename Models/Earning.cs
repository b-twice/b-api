using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Earning")]
    public partial class Earning
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Year { get; set; }
        public long Gross { get; set; }
        public long Taxable { get; set; }
        public long Taxed { get; set; }
    }
}
