using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("RecipeNote")]
    public partial class RecipeNote
    {
        [Key]
        public long Id { get; set; }
        public long RecipeId { get; set; }
        [Required]
        public string Content { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(RecipeId))]
        [InverseProperty("RecipeNotes")]
        public virtual Recipe Recipe { get; set; }
    }
}
