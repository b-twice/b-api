using b.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using b.Data.Models;
using b.Data.Crypto.Read.Accessor;

namespace b.Data.Crypto.Read
{
    public class CryptoInvestmentRds : IReadDataService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly SearchInvestmentsAccessor _searchInvestmentsAccessor;

        public CryptoInvestmentRds(AppDbContext context, IMapper mapper, SearchInvestmentsAccessor searchInvestmentsAccessor)
        {
            _context = context;
            _mapper = mapper;
            _searchInvestmentsAccessor = searchInvestmentsAccessor;
        }

        #region Lookups
        public CryptoHoldingLookupsDto InvestmentLookups()
        {
            return new CryptoHoldingLookupsDto(
                GetCryptoCoins.Execute(_context).ProjectTo<Lookup>(_mapper.ConfigurationProvider).AsEnumerable(),
                new List<Lookup>() { new Lookup { Id = 0, Name = "Active" }, new Lookup { Id = 0, Name = "Sold" } }
            );
        }
        #endregion
        #region Holdings
        public CryptoHolding Holding(int id) =>
            GetHoldingAccessor.Execute(_context, id);

        #nullable enable
        public IEnumerable<CryptoInvestmentDto> SearchInvestments(SearchHoldingsParams? search) =>
            _searchInvestmentsAccessor.Execute(search).ProjectTo<CryptoInvestmentDto>(_mapper.ConfigurationProvider);

        #endregion

        #region Prices
        public IEnumerable<CryptoPrice> LatestPrices() =>
            LatestCoinPricesAccessor.Execute(_context);

        public CryptoPrice LatestPrices(int id) =>
            LatestCoinPriceAccessor.Execute(_context, id);
        #endregion

        }
}
