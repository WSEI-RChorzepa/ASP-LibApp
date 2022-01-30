using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Application.Core.Models;
using LibApp.Application.UseCases.Account.Commands;
using LibApp.Application.UseCases.Account.Queries;
using LibApp.Application.UseCases.MembershipTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    [Authorize(Policy = "AdminAccess")]
    public partial class RegisterModel : PageModel
    {
        private readonly IMediator _mediator;


        public RegisterModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        [BindProperty]
        public List<SelectListItem> MembershipTypes { get; set; }

        [BindProperty]
        public List<SelectListItem> Roles { get; set; }

        private async Task GetApplicationRoles()
        {
            Roles = (await _mediator.Send(new GetRoles.Query()))
               .Select(a => new SelectListItem
               {
                   Value = a.Id.ToString(),
                   Text = a.Name,
                   Selected = false
               }).ToList();
        }

        private async Task GetMembershipTypes()
        {
            MembershipTypes = (await _mediator.Send(new GetMembershipTypes.Query()))
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name,
                }).ToList();
        }

        public async Task OnGetAsync()
        {
            await GetApplicationRoles();
            await GetMembershipTypes();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new RegisterAccount.Command
                {
                    Email = Input.Email,
                    Name = Input.Name,
                    HasNewsletterSubscribed = Input.HasNewsletterSubscribed,
                    MembershipTypeId = Input.MembershipTypeId,
                    Birthdate = Input.Birthdate,
                    Password = Input.Password
                });
 
                if (response.Result.Succeeded)
                {
                    await _mediator.Send(new AssignAccountToRoles.Command
                    {
                        Account = response.Account,
                        SelectedItems = Roles.Select(a => new SelectedItem
                        {
                            Value = a.Text,
                            Selected = a.Selected
                        }).ToList()
                    });

                    TempData["Message"] = $"Account for user with email address: {Input.Email} was successfully created.";
                    return LocalRedirect("~/customers");
                }
                foreach (var error in response.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    await GetMembershipTypes();
                }
            }

            await GetMembershipTypes();
            return Page();
        }
    }
}
