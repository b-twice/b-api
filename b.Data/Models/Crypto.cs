using System;
using System.Collections.Generic;
using b.Entities;

namespace b.Data.Models
{
    #region search
    #nullable enable
    public record SearchHoldingsParams(string? Sort, IList<string>? Coins, IList<int>? YearsSold, string? HoldingStatus);
    #endregion

    #region dtos
    public record CryptoHoldingLookupsDto(IEnumerable<Lookup> Coins, IEnumerable<Lookup> HoldingStatus);

    public record CryptoInvestmentDto
    {
        public long? Id { get; init; }
        public string Name { get; init; }
        public string HoldingStatus { get; init; }
        public DateTime PurchaseDate { get; init; }
        public decimal PurchasePrice { get; init; }
        public decimal PurchaseValue { get; init; }
        public decimal Quantity { get; init; }
        public decimal? MarketValue { get; init; }
        public decimal? SellPrice { get; init; }
        public decimal? NetGain { get; init; }
        public decimal? SellValue { get; init; }
        public DateTime? SellDate { get; init; }
    }
    #endregion
}
