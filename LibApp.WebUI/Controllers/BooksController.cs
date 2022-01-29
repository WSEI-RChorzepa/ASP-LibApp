using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using LibApp.Application.UseCases.Books.Commands;
using LibApp.Application.UseCases.Books.Queries;
using LibApp.WebUI.Models;
using AutoMapper;
using LibApp.Application.UseCases.Genres.Queries;
using LibApp.Application.Core.Exceptions;

namespace LibApp.WebUI.Controllers
{
    public class BooksController : BaseController
    {
        public BooksController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public IActionResult Index() => View();


        public async Task<IActionResult> GetBooks()
        {
            var model = await Mediator.Send(new GetBooks.Query());

            return Ok(model);
        }

        public async Task<IActionResult> New()
        {
            ViewBag.Genres = await Mediator.Send(new GetGenres.Query());

            return View("BookForm", new AddOrUpdateBookFormModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await Mediator.Send(new GetBook.Query { Id = id });

            if (book == null)
                return NotFound();

            ViewBag.Genres = await Mediator.Send(new GetGenres.Query());

            return View("BookForm", Mapper.Map<AddOrUpdateBookFormModel>(book));
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await Mediator.Send(new GetBook.Query { Id = id });
            return View("Details", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AddOrUpdateBookFormModel model)
        {
           try
            {
                await Mediator.Send(model.Id == 0
                   ? Mapper.Map<CreateBook.Command>(model)
                   : Mapper.Map<UpdateBook.Command>(model));
            }
            catch(ValidationException exception)
            {
                ModelState.Clear();

                foreach (var item in exception.Errors)
                    ModelState.AddModelError(item.Property, item.Message);

                ViewBag.Genres = await Mediator.Send(new GetGenres.Query());

                return View("BookForm", model);
            }

            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> RemoveBook(int id)
        {
            await Mediator.Send(new RemoveBook.Command { Id = id });

            return Ok();
        }

    }
}
