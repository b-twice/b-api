using System;
using System.Collections.Generic;

namespace B.API.Models
{
    public partial class User
    {
        public User()
        {
            MealPlans = new HashSet<MealPlan>();
            Recipes = new HashSet<Recipe>();
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<MealPlan> MealPlans { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
