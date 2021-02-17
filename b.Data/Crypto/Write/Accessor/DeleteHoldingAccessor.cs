using System;
using b.Data.Models;
using b.Entities;
using System.Linq;

namespace b.Data.Crypto.Write.Accessor
{
    public class DeleteHoldingAccessor : IWriteAccessor<long>
    {
        private readonly AppDbContext _context;

        public DeleteHoldingAccessor(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(long id)
        {
            var holding = _context.CryptoHoldings.Single(h => h.Id == id);
            var sales = _context.CryptoSales.Where(h => h.CryptoHoldingId == id);
            _context.CryptoSales.RemoveRange(sales);
            _context.CryptoHoldings.Remove(holding);
            _context.SaveChanges();
        }
    }
}
