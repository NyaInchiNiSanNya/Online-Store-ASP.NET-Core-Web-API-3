using AutoMapper;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.MappingProfiles
{
    public class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

        CreateMap<CategoryDto, CreateNewCategoryRequest>().ReverseMap();
        CreateMap<CategoryDto, Сategory>().ReverseMap();
            CreateMap<CategoryDto, GetCategoryByIdResponse>().ReverseMap();
            CreateMap<PatchCategoryRequest, CategoryDto>().
                ForMember(dto => dto.Name,
                    opt
                        => opt.MapFrom(
                            article
                                => article.newName));

        }
    }
}
