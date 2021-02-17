using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace b.Data.Crypto.Read
{
  internal static class LatestCoinPriceAccessor
  {
    public static CryptoPrice Execute(AppDbContext context, int id)
    {
      return context.CryptoPrices
          .Include(c => c.CryptoCoin)
          .AsNoTracking()
          .OrderByDescending(c => c.QueryDate)
          .FirstOrDefault(c => c.CryptoCoinId == id);
    }
  }
}
