using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models
{
    [Keyless]
    public partial class CryptoInvestment
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal? MarketValue { get; set; }
        public string HoldingStatus { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? SellValue { get; set; }
        public decimal? NetGain { get; set; }
        public string SellDate { get; set; }
    }
}
