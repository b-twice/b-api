using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("Post")]
    [Index(nameof(PostGroupId), nameof(Title), nameof(Date), nameof(Path), IsUnique = true)]
    public partial class Post
    {
        [Key]
        public long Id { get; set; }
        public long PostGroupId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Path { get; set; }
        public long Authenticate { get; set; }
        public long Star { get; set; }

        [ForeignKey(nameof(PostGroupId))]
        [InverseProperty("Posts")]
        public virtual PostGroup PostGroup { get; set; }
    }
}
