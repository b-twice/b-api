using System;
using System.Collections.Generic;

#nullable disable

namespace b.Entities
{
    public partial class VCryptoInvestment
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal Quantity { get; set; }
        public decimal? MarketValue { get; set; }
        public string HoldingStatus { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? NetGain { get; set; }
        public decimal? SellValue { get; set; }
        public DateTime? SellDate { get; set; }
    }
}
