using AutoMapper;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.UserEmail, opt =>
                    opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.OrderItemsDto, opt => opt.MapFrom(src => src.OrderItems)); ;

        }
    }
}
