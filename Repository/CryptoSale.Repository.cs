using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class CryptoSaleRepository
    {
        private readonly AppDbContext _context;

        public CryptoSaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public CryptoSale Find(long id) 
        {
            return Include(_context.CryptoSales.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<CryptoSale> FindAll() 
        {
            return Include(_context.CryptoSales).AsNoTracking();
        }

        public IQueryable<CryptoSale> Include(IQueryable<CryptoSale> cryptoSales) 
        {
            return cryptoSales.Include(b => b.CryptoHolding).ThenInclude(c => c.CryptoCoin);
        }


        public IQueryable<CryptoSale> Order(IQueryable<CryptoSale> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(e => e.Id),
                "id_desc" => items.OrderByDescending(e => e.Id),
                "sellDate_asc" => items.OrderBy(e => e.SellDate),
                "sellDate_desc" => items.OrderByDescending(e => e.SellDate),
                "price_asc" => items.OrderBy(e => e.Price),
                "price_desc" => items.OrderByDescending(e => e.Price),
                "quantity_asc" => items.OrderBy(e => e.Quantity),
                "quantity_desc" => items.OrderByDescending(e => e.Quantity),
               _ => items
            };
            return items;
        }
 

  }
}