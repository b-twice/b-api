using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Investment")]
    public partial class Investment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Year { get; set; }
        public long Saving { get; set; }
        public long Hsa { get; set; }
        public long Ira { get; set; }
        public long Roth { get; set; }
        public long Stock { get; set; }
    }
}
