using AutoMapper;
using LibApp.Application.Core.Dtos;
using LibApp.Application.UseCases.Books.Commands;
using LibApp.Application.UseCases.Customers.Commands;
using LibApp.WebUI.Models;

namespace LibApp.WebUI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Book

            CreateMap<BookDto, AddOrUpdateBookFormModel>();
            CreateMap<AddOrUpdateBookFormModel, CreateBook.Command>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));
            CreateMap<AddOrUpdateBookFormModel, UpdateBook.Command>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));

            #endregion

            #region Customer

            CreateMap<CustomerDto, AddOrUpdateCustomerFormModel>();
            CreateMap<AddOrUpdateCustomerFormModel, CreateCustomer.Command>().ForMember(dest => dest.MembershipTypeId, opt => opt.MapFrom(src => src.MembershipTypeId));
            CreateMap<AddOrUpdateCustomerFormModel, UpdateCustomer.Command>().ForMember(dest => dest.MembershipTypeId, opt => opt.MapFrom(src => src.MembershipTypeId));

            #endregion
        }
    }
}
