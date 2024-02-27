using AutoMapper;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<CategoryDto, Сategory>().ReverseMap();
        }
    }
}
