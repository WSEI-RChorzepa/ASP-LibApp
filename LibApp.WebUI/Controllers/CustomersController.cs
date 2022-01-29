using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using LibApp.Application.UseCases.Customers.Queries;
using System.Threading.Tasks;
using LibApp.Application.UseCases.MembershipTypes.Queries;
using LibApp.WebUI.Models;
using LibApp.Application.UseCases.Customers.Commands;
using LibApp.Application.Core.Exceptions;

namespace LibApp.WebUI.Controllers
{

    public class CustomersController : BaseController
    {
        public CustomersController(IMediator mediator, IMapper mapper) 
            : base(mediator, mapper)
        {
        }

        public ViewResult Index() => View();

        public async Task<IActionResult> GetCustomers()
        {
            var model = await Mediator.Send(new GetCustomers.Query());

            return Ok(model);
        }

        public async Task<IActionResult> New()
        {
            ViewBag.MembershipTypes = await Mediator.Send(new GetMembershipTypes.Query());

            return View("CustomerForm", new AddOrUpdateCustomerFormModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await Mediator.Send(new GetCustomer.Query { Id = id });

            if (customer == null)
                return NotFound();

            ViewBag.MembershipTypes = await Mediator.Send(new GetMembershipTypes.Query());

            return View("CustomerForm", Mapper.Map<AddOrUpdateCustomerFormModel>(customer));
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await Mediator.Send(new GetCustomer.Query { Id = id });
            return View("Details", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AddOrUpdateCustomerFormModel model)
        {
            try
            {
                await Mediator.Send(model.Id.HasValue
                 ? Mapper.Map<UpdateCustomer.Command>(model)
                 : Mapper.Map<CreateCustomer.Command>(model));
            }
            catch (ValidationException exception)
            {
                ModelState.Clear();

                foreach (var item in exception.Errors)
                    ModelState.AddModelError(item.Property, item.Message);

                ViewBag.MembershipTypes = await Mediator.Send(new GetMembershipTypes.Query());

                return View("CustomerForm", model);
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}