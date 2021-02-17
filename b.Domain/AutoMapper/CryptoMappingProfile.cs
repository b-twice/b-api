using System;
using b.Entities;
using AutoMapper;
using b.Domain.Crypto;
using Investment = b.Domain.Crypto.Investment;

namespace b.Domain.AutoMapper
{
  public class CryptoDomainMappingProfile : Profile
  {
    public CryptoDomainMappingProfile()
    {
      CreateMap<CryptoHolding, Investment>()
          .ForMember(dest => dest.CoinName, o => o.MapFrom(src => src.CryptoCoin.Name))
          .ForMember(dest => dest.CoinId, o => o.MapFrom(src => src.CryptoCoin.Id));

      CreateMap<CryptoPrice, CoinPrice>()
          .ForMember(dest => dest.CoinId, o => o.MapFrom(src => src.CryptoCoin.Id))
          .ForMember(dest => dest.CoinName, o => o.MapFrom(src => src.CryptoCoin.Name))
          .ForMember(dest => dest.MarketPrice, o => o.MapFrom(src => src.Price))
          .ForMember(dest => dest.Date, o => o.MapFrom(src => src.QueryDate));

      //CreateMap<PricedInvestment, CryptoInvestmentDto>()
      //.ForMember(dest => dest.Id, o => o.MapFrom(src => src.Investment.Id))
      //.ForMember(dest => dest.CoinName, o => o.MapFrom(src => src.Investment.CoinName))
      //.ForMember(dest => dest.PurchasePrice, o => o.MapFrom(src => src.Investment.PurchasePrice))
      //.ForMember(dest => dest.PurchaseDate, o => o.MapFrom(src => src.Investment.PurchaseDate))
      //.ForMember(dest => dest.Quantity, o => o.MapFrom(src => src.Investment.Quantity))
      //.ForMember(dest => dest.PurchaseCost, o => o.MapFrom(src => src.Investment.PurchaseCost))
      //.ForMember(dest => dest.MarketPrice, o => o.MapFrom(src => src.MarketValue))
      //.ForMember(dest => dest.PercentGrowth, o => o.MapFrom(src => src.PercentGrowth));

    }
  }
}
