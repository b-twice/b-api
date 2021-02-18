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
        private readonly CryptoCoinsAccessor _cryptoCoinsAccessor;
        private readonly LatestCoinPricesAccessor _latestCoinPricesAccessor;
        private readonly AnnualSummaryAccessor _annualSummaryAccessor;


        public CryptoInvestmentRds(
            AppDbContext context,
            IMapper mapper,
            SearchInvestmentsAccessor searchInvestmentsAccessor,
            CryptoCoinsAccessor cryptoCoinsAccessor,
            LatestCoinPricesAccessor latestCoinPricesAccessor,
            AnnualSummaryAccessor annualSummaryAccessor
        )
        {
            _context = context;
            _mapper = mapper;
            _searchInvestmentsAccessor = searchInvestmentsAccessor;
            _cryptoCoinsAccessor = cryptoCoinsAccessor;
            _latestCoinPricesAccessor = latestCoinPricesAccessor;
            _annualSummaryAccessor = annualSummaryAccessor;
        }

        public CryptoHoldingLookupsDto InvestmentLookups() =>
            new CryptoHoldingLookupsDto(
                _cryptoCoinsAccessor.Execute().ProjectTo<Lookup>(_mapper.ConfigurationProvider).AsEnumerable(),
                new List<Lookup>() { new Lookup { Id = 0, Name = "Active" }, new Lookup { Id = 0, Name = "Sold" } }
            );

        public CryptoInvestmentSummaryDto InvestmentSummary() =>
            new CryptoInvestmentSummaryDto { 
                AnnualInvestmentSummaries = _annualSummaryAccessor.Execute().ProjectTo<CryptoAnnualInvestmentSummaryDto>(_mapper.ConfigurationProvider).AsEnumerable(),
                LatestCoinPrices = _latestCoinPricesAccessor.Execute().ProjectTo<CoinPriceDto>(_mapper.ConfigurationProvider).AsEnumerable()
            };

        #nullable enable
        public IEnumerable<CryptoInvestmentDto> SearchInvestments(SearchHoldingsParams? search) =>
            _searchInvestmentsAccessor.Execute(search).ProjectTo<CryptoInvestmentDto>(_mapper.ConfigurationProvider);

    }
}
