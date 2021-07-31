using System.Collections.Generic;
using AutoMapper;
using crm.API.Mapping.Dtos;
using crm.Models;

namespace crm.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Currency, CurrencyDto>()
                .ForMember(dest => dest.Updated_at, opt => opt.MapFrom(src => src.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<List<Currency>, CurrenciesDto>()
                .ForMember(dest => dest.Currencies, opt => opt.MapFrom(src => src));
        }
    }
}
