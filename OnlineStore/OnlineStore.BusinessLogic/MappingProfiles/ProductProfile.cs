using AutoMapper;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<ProductDto, Product>().ReverseMap();

        }
    }
}
