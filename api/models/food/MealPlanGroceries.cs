using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class MealPlanGrocery
    {
        public string mealPlanName { get; set; }
        public int mealPlanId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public decimal count { get; set; }
        public decimal weight { get; set; }
        public string unit { get; set; }
        public int dirty { get; set; }
        public string supermarket { get; set; }
        public string supermarketName { get; set; }
   }
}






