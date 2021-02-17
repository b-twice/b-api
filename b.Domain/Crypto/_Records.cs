using System;
using System.Collections.Generic;
using System.Linq;

namespace b.Domain.Crypto
{
    internal record CoinPrice
    {
        public long CoinId { get; init; }
        public string CoinName { get; init; }
        public decimal MarketPrice { get; init; }
        public DateTime Date { get; init; }
    }
    internal record Investment
    {
        public long Id { get; init; }
        public long CoinId { get; init; }
        public string CoinName { get; init; }
        public DateTime PurchaseDate { get; init; }
        public decimal PurchasePrice { get; init; }
        public decimal Quantity { get; init; }
        public decimal PurchaseCost
        {
            get => PurchasePrice * Quantity;
        }
    }
    internal record InvestmentSale
    {
        public long Id { get; init; }
        public long InvestmentId { get; init; }
        public DateTime SellDate { get; init; }
        public decimal Quantity { get; init; }
        public decimal SellPrice { get; init; }
    }
    internal record InvestmentSummary {
        public Investment Investment { get; init; }
        public decimal MarketValue { get; init; }
        public decimal PercentGrowth { get; init; }
        public decimal PurchaseValue { get; init; }
    };
    internal record SoInvestment
    {
        public decimal PurchaseDate { get; init; }
        public decimal PurchaseQuantity { get; init; }
        public decimal PurchaseValue { get; init; }
        public decimal SellQuantity { get; init; }
        public decimal SellValue { get; init; }
        public decimal Return { get; init; }
        public decimal PercentReturn { get; init; }
    };

}
