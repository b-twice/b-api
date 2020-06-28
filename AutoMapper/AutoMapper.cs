using B.API.Models;
using AutoMapper;
using System.Collections.Generic;

namespace B.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, FinancialSummary>()
                .ForMember(dest => dest.Asset, o => o.MapFrom(src => src));
            CreateMap<Investment, FinancialSummary>()
                .ForMember(dest => dest.Investment, o => o.MapFrom(src => src));
            CreateMap<Debt, FinancialSummary>()
                .ForMember(dest => dest.Debt, o => o.MapFrom(src => src));
            CreateMap<Earning, FinancialSummary>()
                .ForMember(dest => dest.Earnings, o => o.MapFrom(src => src));

        }
    }
}