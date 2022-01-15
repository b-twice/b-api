using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Keyless]
    public partial class CryptoAnnualInvestmentSummary
    {
        public string InvestmentYear { get; set; }
        public decimal MarketValue { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal SellValue { get; set; }
        public decimal NetGain { get; set; }
    }
}
