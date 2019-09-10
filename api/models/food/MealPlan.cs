using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class MealPlan
    {

        public int id { get; set; }
        public string user { get; set; }
        public string name { get; set; }
        public int days { get; set; }
        public decimal? cost { get; set; }
        public decimal? costOrganic { get; set; }
        public decimal? costSeasonal { get; set; }

    }
}



