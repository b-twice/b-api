
using System.Collections.Generic;
using b.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using b.Data.Models;

namespace b.Data.Crypto.Read.Accessor
{
    public class AnnualSummaryAccessor : IReadAccessor<IQueryable<VCryptoAnnualInvestmentSummary>>
    {
        private readonly AppDbContext _context;
        public AnnualSummaryAccessor(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<VCryptoAnnualInvestmentSummary> Execute()
        {
            return _context.VCryptoAnnualInvestmentSummaries.AsNoTracking();
        }

    }
}
