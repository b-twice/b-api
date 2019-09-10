using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class Recipe
    {
        public int id { get; set; }
        public string user { get; set; }
        public string cookbook { get; set; }
        public string category{ get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int servings { get; set; }
        public int pageNumber { get; set; }
        public decimal servingCost { get; set; }
        public decimal cost { get; set; }
        public decimal costOrganic { get; set; }
        public decimal costSeasonal { get; set; }
   }
}





