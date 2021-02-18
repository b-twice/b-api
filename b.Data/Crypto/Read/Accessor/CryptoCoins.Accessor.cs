using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using b.Data.Models;    

namespace b.Data.Crypto.Read.Accessor
{
    public class CryptoCoinsAccessor : IReadAccessor<IQueryable<CryptoCoin>>
    {
        private readonly AppDbContext _context;
        public CryptoCoinsAccessor(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<CryptoCoin> Execute()
        {
            return _context.CryptoCoins.AsNoTracking();
        }
    }
}
