using AutoMapper;
using LibApp.Domain.Entities;
using LibApp.WebUI.Dtos;

namespace LibApp.WebUI.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
