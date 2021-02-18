using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using b.Data.Models;

namespace b.Data.Crypto.Read.Accessor
{
    public class LatestCoinPricesAccessor : IReadAccessor<IQueryable<VLatestCryptoCoinPrice>>
    {
        private readonly AppDbContext _context;
        public LatestCoinPricesAccessor(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<VLatestCryptoCoinPrice> Execute()
        {
            return _context.VLatestCryptoCoinPrices.AsNoTracking();
        }

    }
}
