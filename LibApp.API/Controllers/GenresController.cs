using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.API.Controllers
{
    public class GenresController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;

        public GenresController(IMapper mapper, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var entities = await _genreRepository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<GenreDto>());

            var genres = _mapper.Map<List<GenreDto>>(entities);

            return Ok(genres);
        }
    }
}
