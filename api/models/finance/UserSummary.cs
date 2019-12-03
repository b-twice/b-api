using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace B.API.Models.Finance
{
    public class UserSummary
    {
        public int fiscalYear{ get; set; }
        public string userName { get; set; }

        public int income { get; set; }
        public int incomeGrowth { get; set; }

        public int incomeTaxable { get; set; }
        public int incomeTaxableGrowth { get; set; }

        public int takeHomePay { get; set; }
        public int takeHomePayGrowth { get; set; }

        public int spent { get; set; }
        public int spentGrowth{ get; set; }

        public int saved{ get; set; }
        public int savedGrowth{ get; set; }

        public int retirementContribution{ get; set; }
        public int retirementContributionGrowth{ get; set; }

        public int stockContribution{ get; set; }
        public int stockContributionGrowth{ get; set; }
 
        public int retirement{ get; set; }
        public int retirementGrowth{ get; set; }

        public int stock{ get; set; }
        public int stockGrowth{ get; set; }
 
        public int saving { get; set; }
        public int savingGrowth { get; set; }

        public int taxed{ get; set; }
        public int taxedGrowth{ get; set; }

        public int debt{ get; set; }
        public int debtGrowth{ get; set; }
    }
}


