using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class CryptoHoldingRepository
    {
        private readonly AppDbContext _context;

        public CryptoHoldingRepository(AppDbContext context)
        {
            _context = context;
        }

        public CryptoHolding Find(long id) 
        {
            return Include(_context.CryptoHoldings.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<CryptoHolding> FindAll() 
        {
            return Include(_context.CryptoHoldings).AsNoTracking();
        }

        public IQueryable<CryptoHolding> Include(IQueryable<CryptoHolding> cryptoHoldings) 
        {
            return cryptoHoldings.Include(b => b.CryptoCoin);
        }


        public IQueryable<CryptoHolding> Order(IQueryable<CryptoHolding> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(e => e.Id),
                "id_desc" => items.OrderByDescending(e => e.Id),
                "purchaseDate_asc" => items.OrderBy(e => e.PurchaseDate),
                "purchaseDate_desc" => items.OrderByDescending(e => e.PurchaseDate),
                "purchasePrice_asc" => items.OrderBy(e => e.PurchasePrice),
                "purchasePrice_desc" => items.OrderByDescending(e => e.PurchasePrice),
                "quantity_asc" => items.OrderBy(e => e.Quantity),
                "quantity_desc" => items.OrderByDescending(e => e.Quantity),
                "cryptoCoinId_asc" => items.OrderBy(e => e.CryptoCoin.Name),
                "cryptoCoinId_desc" => items.OrderByDescending(e => e.CryptoCoin.Name),
               _ => items
            };
            return items;
        }
 

  }
}