using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace b.Data.Crypto.Read
{
    internal static class GetCryptoCoins
    {
        public static IQueryable<CryptoCoin> Execute(AppDbContext context)
        {
            return context.CryptoCoins.AsNoTracking();
        }
    }
}
