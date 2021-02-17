using System;
namespace b.Domain.Crypto
{
    public record CryptoInvestmentDto
    {
        public long Id { get; init; }
        public string CoinName { get; init; }
        public DateTime PurchaseDate { get; init; }
        public decimal PurchasePrice { get; init; }
        public decimal Quantity { get; init; }
        public decimal PurchaseCost { get; init; }
        public decimal MarketPrice { get; init; }
        public decimal PercentGrowth { get; init; }
    }

}
