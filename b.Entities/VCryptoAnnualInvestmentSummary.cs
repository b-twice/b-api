using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class VCryptoAnnualInvestmentSummary
    {
        public int InvestmentYear { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? PurchaseValue { get; set; }
        public decimal? SellValue { get; set; }
        public decimal? NetGain { get; set; }
    }
}
