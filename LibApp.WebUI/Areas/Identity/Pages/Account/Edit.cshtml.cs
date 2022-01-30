using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Models;
using LibApp.Application.UseCases.Account.Commands;
using LibApp.Application.UseCases.Account.Queries;
using LibApp.Application.UseCases.MembershipTypes.Queries;
using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    [Authorize(Policy = "AdminAccess")]
    public partial class EditModel : PageModel
    {
        private readonly IMediator _mediator;


        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public EditInputModel Input { get; set; }

        [BindProperty]
        public List<SelectListItem> MembershipTypes { get; set; }

        [BindProperty]
        public List<SelectListItem> Roles { get; set; }

        [BindProperty]
        public bool AccountIsExist { get; set; } = false;

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

        public async Task OnGetAsync(int id)
        {
            var account = await _mediator.Send(new GetAccount.Query { Id = id });

            await GetApplicationRoles();
            await GetMembershipTypes();

            if (account == null)
                ModelState.AddModelError("Id", $"Customer with id: {id} was not found");
            else
            {
                Input = new EditInputModel
                {
                    Id = account.Id,
                    Name = account.Name,
                    HasNewsletterSubscribed = account.HasNewsletterSubscribed,
                    MembershipTypeId = account.MembershipTypeId,
                    Birthdate = account.Birthdate,
                };

                var userRoles = await _mediator.Send(new GetRolesByAccount.Query { Account = account });

                foreach (var role in Roles)
                {
                    if (userRoles.Contains(role.Text))
                        role.Selected = true;
                }

                AccountIsExist = true;
            }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (TryValidateModel(Input))
            {
                var response = await _mediator.Send(new UpdateAccount.Command
                {
                    Id = Id,
                    Name = Input.Name,
                    HasNewsletterSubscribed = Input.HasNewsletterSubscribed,
                    MembershipTypeId = Input.MembershipTypeId,
                    Birthdate = Input.Birthdate
                });

                if (response.Result.Succeeded)
                {
                    await _mediator.Send(new UpdateAccountRoles.Command
                    {
                        Account = response.Account,
                        SelectedItems = Roles.Select(a => new SelectedItem
                        {
                            Value = a.Text,
                            Selected = a.Selected
                        }).ToList()
                    });


                    TempData["Message"] = $"Account for user with email address: {response.Account.Email} was successfully updated.";
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
