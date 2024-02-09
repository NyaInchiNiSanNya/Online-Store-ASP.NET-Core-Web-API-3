using AutoMapper;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductDtoProfile : Profile
    {
        public ProductDtoProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<ProductDto, CreateNewProductRequest>().ReverseMap();
            CreateMap<ProductDto, GetProductByIdResponse>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<PatchProductRequest, ProductDto>().
                ForMember(dto => dto.Name,
                    opt
                        => opt.MapFrom(
                            article
                                => article.NewName)).
                ForMember(dto => dto.Description,
                    opt
                        => opt.MapFrom(
                            article
                                => article.NewDescription)).
                ForMember(dto => dto.Price,
                    opt
                        => opt.MapFrom(
                            article
                                => article.NewPrice));

        }
    }
}
