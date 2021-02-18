using System;
using b.Entities;
using AutoMapper;
using b.Data.Models;

namespace b.Data.AutoMapper
{
      public class CryptoDataMappingProfile : Profile
      {
            public CryptoDataMappingProfile()
            {
                CreateMap<VCryptoInvestment, CryptoInvestmentDto>();
                CreateMap<CryptoCoin, Lookup>();
                CreateMap<VCryptoAnnualInvestmentSummary, CryptoAnnualInvestmentSummaryDto>();
                CreateMap<VLatestCryptoCoinPrice, CoinPriceDto>();

        }
    }
}
