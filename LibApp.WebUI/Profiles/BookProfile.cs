using AutoMapper;
using LibApp.Domain.Entities;
using LibApp.WebUI.Dtos;

namespace LibApp.WebUI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
