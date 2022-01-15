using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Keyless]
    public partial class MealPlanGrocery
    {
        public string MealPlanName { get; set; }
        public long? MealPlanId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Count { get; set; }
        public decimal? Weight { get; set; }
        public string Unit { get; set; }
        public long? Dirty { get; set; }
        public string Supermarket { get; set; }
        public string SupermarketName { get; set; }
    }
}
