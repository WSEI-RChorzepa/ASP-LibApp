using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IBookRespository _bookRespository;

        public BooksController(IMapper mapper, IBookRespository bookRespository)
        {
            _mapper = mapper;
            _bookRespository = bookRespository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var entities = await _bookRespository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<BookDto>());

            var books = _mapper.Map<List<BookDto>>(entities);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var entity = await _bookRespository.GetAsync(id);

            if (entity == null)
                return NotFound();

            var book = _mapper.Map<BookDto>(entity);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            var model = _mapper.Map<Book>(bookDto);

            if (!TryValidateModel(model, nameof(Book)))
                return BadRequest(ModelState);

            await _bookRespository.CreateAsync(model);

            return CreatedAtAction(nameof(GetBook), new { id = model.Id }, model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody]BookDto bookDto)
        {
            var bookInDb = await _bookRespository.GetAsync(bookDto.Id);

            if (bookInDb == null)
                return NotFound();

            _mapper.Map(bookDto, bookInDb);

            if (!TryValidateModel(bookInDb, nameof(Book)))
                return BadRequest(ModelState);

            await _bookRespository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var entity = await _bookRespository.GetAsync(id);

            if (entity == null)
                return NotFound();

            await _bookRespository.DeleteAsync(id);

            return Ok();
        }
    }
}
