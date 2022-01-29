using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.WebUI.Controllers.Api
{
    public class BooksController : BaseApiController
    {
        private readonly IBookRespository _bookRespository;
        public BooksController(IMapper mapper, IBookRespository bookRespository) : base(mapper)
        {
            _bookRespository = bookRespository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var entities = await _bookRespository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<BookDto>());

            var books = Mapper.Map<List<BookDto>>(entities);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var entity = await _bookRespository.GetAsync(id);

            if (entity == null)
                return NotFound();

            var book = Mapper.Map<BookDto>(entity);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            var model = Mapper.Map<Book>(bookDto);

            if (!TryValidateModel(model, nameof(Book)))
                return BadRequest(ModelState);

            await _bookRespository.CreateAsync(model);

            return CreatedAtAction(nameof(GetBook), new { id = model.Id }, model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto)
        {
            var bookInDb = await _bookRespository.GetAsync(bookDto.Id);

            if (bookInDb == null)
                return NotFound();

            Mapper.Map(bookDto, bookInDb);

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
