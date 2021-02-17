using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace b.Data.Crypto.Read
{
  internal static class GetHoldingAccessor
  {
    public static CryptoHolding Execute(AppDbContext context, int id)
    {
      return context.CryptoHoldings.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }
  }
}
