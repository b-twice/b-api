using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Table("User")]
    [Index(nameof(Email), IsUnique = true)]
    public partial class User
    {
        public User()
        {
            MealPlans = new HashSet<MealPlan>();
            Recipes = new HashSet<Recipe>();
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Name {
            get 
            {
                return $"{FirstName} {LastName}";
            }
        
        }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(MealPlan.User))]
        public virtual ICollection<MealPlan> MealPlans { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(Recipe.User))]
        public virtual ICollection<Recipe> Recipes { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(TransactionRecord.User))]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
