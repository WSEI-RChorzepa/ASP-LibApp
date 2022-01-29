using AutoMapper;
using LibApp.Domain.Entities;
using LibApp.WebUI.Dtos;

namespace LibApp.WebUI.Profiles
{
    public class MembershipTypeProfile : Profile
    {
        public MembershipTypeProfile()
        {
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();
        }
    }
}
