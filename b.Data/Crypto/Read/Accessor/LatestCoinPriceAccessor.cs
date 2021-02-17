using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace b.Data.Crypto.Read
{
  internal static class LatestCoinPricesAccessor
  {
    public static IEnumerable<CryptoPrice> Execute(AppDbContext context)
    {
      return context.CryptoPrices.Include(c => c.CryptoCoin)
          .AsNoTracking()
          .AsEnumerable()
          .GroupBy(c => c.CryptoCoinId)
          .Select(g => g.OrderBy(c => c.QueryDate).First());

    }
  }
}
