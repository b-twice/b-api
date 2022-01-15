using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class CryptoInvestmentRepository
    {
        private readonly AppDbContext _context;

        public CryptoInvestmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public CryptoInvestment Find(long id) 
        {
            return FindAll().First(b => b.Id == id);
        }

        
        public IQueryable<CryptoInvestment> FindAll() 
        {
            return _context.CryptoInvestments.AsNoTracking();
        }



        public IQueryable<CryptoInvestment> Order(IQueryable<CryptoInvestment> items, string sortName) 
        {
            items = sortName.ToLower() switch
            {
                "name_asc" => items.OrderBy(e => e.Name),
                "name_desc" => items.OrderByDescending(e => e.Name),
                "purchasedate_asc" => items.OrderBy(e => e.PurchaseDate),
                "purchasedate_desc" => items.OrderByDescending(e => e.PurchaseDate),
                "purchaseprice_asc" => items.OrderBy(e => e.PurchasePrice),
                "purchaseprice_desc" => items.OrderByDescending(e => e.PurchasePrice),
                "purchasevalue_asc" => items.OrderBy(e => e.PurchaseValue),
                "purchasevalue_desc" => items.OrderByDescending(e => e.PurchaseValue),
                "quantity_asc" => items.OrderBy(e => e.Quantity),
                "quantity_desc" => items.OrderByDescending(e => e.Quantity),
                "marketvalue_asc" => items.OrderBy(e => e.MarketValue),
                "marketvalue_desc" => items.OrderByDescending(e => e.MarketValue),
                "sellprice_asc" => items.OrderBy(e => e.SellPrice),
                "sellprice_desc" => items.OrderByDescending(e => e.SellPrice),
                "sellvalue_asc" => items.OrderBy(e => e.SellValue),
                "sellvalue_desc" => items.OrderByDescending(e => e.SellValue),
                "netgain_asc" => items.OrderBy(e => e.NetGain),
                "netgain_desc" => items.OrderByDescending(e => e.NetGain),
                "status_asc" => items.OrderBy(e => e.HoldingStatus),
                "status_desc" => items.OrderByDescending(e => e.HoldingStatus),
                _ => items
            };

            return items;
        }
 
        public IQueryable<CryptoInvestment> Filter(IQueryable<CryptoInvestment> items, List<string> coins, List<string> yearsSold, string holdingStatus)
        {
            if (coins != null && coins.Any())
            {
                items = items.Where(c => coins.Contains(c.Name));
            }
            if (yearsSold?.Any() == true)
            {
                items = items.Where(i => yearsSold.Contains(i.SellDate.Substring(0,4)));
            }
            if (holdingStatus != null)
            {
                items = items.Where(i => holdingStatus == i.HoldingStatus);
            }
            return items;
        }

  }
}
