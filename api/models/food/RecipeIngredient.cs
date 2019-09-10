using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class RecipeIngredient
    {
        public int id { get; set; }
        public string recipe { get; set; }
        public string name { get; set; }
        public decimal count { get; set; }
        public decimal weight { get; set; }
        public string unit { get; set; }
        public string measurement { get; set; }
        public decimal cost { get; set; }
        public decimal costOrganic { get; set; }
        public decimal costSeasonal { get; set; }
   }
}






