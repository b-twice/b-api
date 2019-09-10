using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Food
{
    public class Grocery
    {
        public int id { get; set; }
        public string user { get; set; }
        public string supermarket { get; set; }
        public string category{ get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public decimal count { get; set; }
        public decimal weight { get; set; }
        public string unit{ get; set; }
        public string organic{ get; set; }
        public string seasonal{ get; set; }
        public decimal amount { get; set; }
        public decimal unitPrice { get; set; }
        public string quantityType { get; set; }
   }
}




