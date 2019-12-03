using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace B.API.Models.Finance
{
    public class MonthlyExpense
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal amount { get; set; }
   }
}




