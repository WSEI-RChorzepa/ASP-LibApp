using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    [Authorize(Policy ="AdminAccess")]
    public partial class RegisterModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public RegisterModel(
            UserManager<Customer> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            ILogger<RegisterModel> logger,
            IMembershipTypeRepository membershipTypeRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _membershipTypeRepository = membershipTypeRepository;
            _roleManager = roleManager;
        }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        [BindProperty]
        public List<SelectListItem> MembershipTypes { get; set; }

        [BindProperty]
        public List<SelectListItem> Roles { get; set; }

        private async Task GetApplicationRoles()
        {
            Roles = (await _roleManager.Roles.ToListAsync())
               .Select(a => new SelectListItem
               {
                   Value = a.Id.ToString(),
                   Text = a.Name,
                   Selected = false
               }).ToList();
        }

        private async Task GetMembershipTypes()
        {
            MembershipTypes = (await _membershipTypeRepository.BrowseAsync()).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
            }).ToList();
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            await GetApplicationRoles();
            await GetMembershipTypes();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new Customer { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Name = Input.Name,
                    HasNewsletterSubscribed = Input.HasNewsletterSubscribed,
                    MembershipTypeId = Input.MembershipTypeId,
                    Birthdate = Input.Birthdate,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var selectedRoles = Roles
                        .Where(a => a.Selected)
                        .ToList();

                    if (selectedRoles.Any())
                    {
                        foreach (var item in selectedRoles)
                            await _userManager.AddToRoleAsync(user, item.Text);
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
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
