using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Finance
{
    public class TransactionMonthly
    {
        public string user { get; set; }
        public int year { get; set; }
        public string month { get; set; }
        public string category { get; set; }
        public int amount { get; set; }
   }
}




