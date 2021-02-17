using System;
using System.Collections.Generic;
using b.Data.Crypto.Read;
using b.Entities;
using System.Linq;

namespace b.Domain.Crypto
{
  internal class CryptoInvestmentManager
  {
    public CryptoInvestmentManager()
    {

    }

    //internal IEnumerable<PricedInvestment> PriceCryptoHoldings(IEnumerable<Investment> investments, IEnumerable<CoinPrice> latestPrices)
    //{
    //  var priceDict = latestPrices.ToDictionary(c => c.CoinId);

    //  var pricedInvestments = (
    //      from investment in investments
    //      let value = priceDict[investment.CoinId].MarketPrice * investment.Quantity
    //      let growth = ((value - investment.PurchaseCost) / value) * 100
    //      select new PricedInvestment { Investment = investment with { }, MarketValue = value, PercentGrowth = growth }
    //  );
    //  return pricedInvestments;
    //}

  }
}
