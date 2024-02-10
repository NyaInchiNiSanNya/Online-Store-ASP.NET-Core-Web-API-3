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
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<UserRegistrationDto, RegistrationRequest>().ReverseMap();

            CreateMap<UserLoginDto, LoginRequest>().ReverseMap();
        }
    }
}
