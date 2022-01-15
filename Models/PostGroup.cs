using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("PostGroup")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class PostGroup
    {
        public PostGroup()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        [InverseProperty(nameof(Post.PostGroup))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
