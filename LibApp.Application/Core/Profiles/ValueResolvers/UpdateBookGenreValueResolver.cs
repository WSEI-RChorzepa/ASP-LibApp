using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Application.UseCases.Books.Commands;

namespace LibApp.Application.Core.Profiles.ValueResolvers
{
    public class UpdateBookGenreValueResolver : IValueResolver<UpdateBook.Command, BookDto, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateBookGenreValueResolver(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public GenreDto Resolve(UpdateBook.Command source, BookDto destination, GenreDto destMember, ResolutionContext context)
        {
            var genre = _genreRepository.GetAsync(source.GenreId).Result;
            
            return context.Mapper.Map<GenreDto>(genre);
        }
    }
}
