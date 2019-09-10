using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Budget.API.Models.Finance
{
    public class UserProfile 
    {

        public string name { get; set; }

        public int income { get; set; }

        public int debt { get; set; }

        public int asset { get; set; }
    }
}

