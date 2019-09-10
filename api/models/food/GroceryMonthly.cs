using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class GroceryMonthly
    {
        public string user { get; set; }
        public int year { get; set; }
        public string month { get; set; }
        public string category { get; set; }
        public int amount { get; set; }
   }
}




