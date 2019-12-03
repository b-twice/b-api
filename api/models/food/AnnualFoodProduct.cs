using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace B.API.Models.Food
{
    public class AnnualFoodProduct
    {
        public string user { get; set; }
        public string category{ get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public int count { get; set; }
        public decimal weight { get; set; }
        public string unit{ get; set; }
        public decimal amount { get; set; }
        public decimal unitPrice { get; set; }
        public int dirty { get; set; }
   }
}




