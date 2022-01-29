using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.WebUI.Controllers.Api
{
    public class GenresController : BaseApiController { 
    
        private readonly IGenreRepository _genreRepository;

        public GenresController(IMapper mapper, IGenreRepository genreRepository) : base(mapper)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var entities = await _genreRepository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<GenreDto>());

            var genres = Mapper.Map<List<GenreDto>>(entities);

            return Ok(genres);
        }
    }
}
