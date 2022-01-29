using LibApp.Application.UseCases.Books.Queries;
using LibApp.Application.UseCases.Customers.Queries;
using LibApp.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibApp.WebUI.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> New()
        {
            var customers = await _mediator.Send(new GetCustomers.Query());
            var books = await _mediator.Send(new GetBooks.Query());

            var viewModel = new RentalsViewModel
            {
                Customers = customers,
                Books = books
            };

            return View(viewModel);
        }
    }
}
