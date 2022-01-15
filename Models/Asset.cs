using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Asset")]
    public partial class Asset
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Year { get; set; }
        public long Saving { get; set; }
        public long Hsa { get; set; }
        public long Retirement { get; set; }
        public long Stock { get; set; }
        public long Home { get; set; }
        public long Auto { get; set; }
    }
}
