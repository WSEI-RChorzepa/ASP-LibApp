using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LibApp.Domain.Entities;

namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public partial class LoginModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<Customer> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<Customer> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, $"Account with email address: {Input.Email} is not exist.");
                    return Page();
                }

                var password = await _signInManager.CheckPasswordSignInAsync(user, Input.Password, false);

                if (password.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                } else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password.");
                    return Page();
                }

            }

            return Page();
        }
    }
}
