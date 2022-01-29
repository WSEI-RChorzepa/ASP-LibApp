using AutoMapper;
using LibApp.Application.Core.Dtos;
using LibApp.Application.Core.Profiles.ValueResolvers;
using LibApp.Application.UseCases.Books.Commands;
using LibApp.Application.UseCases.Customers.Commands;
using LibApp.Domain.Entities;

namespace LibApp.Application.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Book

            CreateMap<Book, Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>().ForMember(dest => dest.Genre, opt => opt.Ignore());
            CreateMap<CreateBook.Command, BookDto>().ForMember(dest => dest.Genre, opt => opt.MapFrom<CreateBookGenreValueResolver>());
            CreateMap<UpdateBook.Command, BookDto>().ForMember(dest => dest.Genre, opt => opt.MapFrom<UpdateBookGenreValueResolver>());

            #endregion

            #region Genre

            CreateMap<Genre, Genre>();
            CreateMap<Genre, GenreDto>().ReverseMap();

            #endregion

            #region MembershipType

            CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();

            #endregion

            #region Customer

            CreateMap<Customer, Customer>().ReverseMap();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>().ForMember(dest => dest.MembershipType, opt => opt.Ignore());

            CreateMap<CreateCustomer.Command, CustomerDto>()
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom<CreateCustomerMembershipTypeGenreValueResolver>());

            CreateMap<UpdateCustomer.Command, CustomerDto>()
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom<UpdateCustomerMembershipTypeGenreValueResolver>());

            #endregion
        }
    }
}
