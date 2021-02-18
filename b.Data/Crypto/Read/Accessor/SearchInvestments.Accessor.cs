using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using b.Data.Models;

namespace b.Data.Crypto.Read.Accessor
{

    public class SearchInvestmentsAccessor : IReadAccessor<SearchHoldingsParams, IQueryable<VCryptoInvestment>>
    {
        private readonly AppDbContext _context;
        public SearchInvestmentsAccessor(AppDbContext context)
        {
             _context = context;
        }

        #nullable enable
        public IQueryable<VCryptoInvestment> Execute(SearchHoldingsParams? search)
        {
            return Search(Query(), search);
        }

        private IQueryable<VCryptoInvestment> Query()
        {
            return _context.VCryptoInvestments.AsNoTracking();
        }

        #nullable enable
        private IQueryable<VCryptoInvestment> Search(IQueryable<VCryptoInvestment> items, SearchHoldingsParams? search)
        {
            if (search == null) return items;

            if (search.Coins != null && search.Coins.Any())
            {
                items = items.Where(c => search.Coins.Contains(c.Name));
            }
            if (search.YearsSold?.Any() == true)
            {
                items = items.Where(i => i.SellDate.HasValue && search.YearsSold.Contains(i.SellDate.Value.Year));
            }
            if (search.HoldingStatus != null)
            {
                items = items.Where(i => search.HoldingStatus == i.HoldingStatus);
            }
            if (!string.IsNullOrEmpty(search.Sort))
            {
                items = search.Sort.ToLower() switch
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
            };
            return items;
        }

    }
}
