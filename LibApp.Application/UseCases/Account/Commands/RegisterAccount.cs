using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Commands
{
    public class RegisterAccount
    {
        public class Command : IRequest<(IdentityResult Result, Customer Account)>
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public bool HasNewsletterSubscribed { get; set; }
            public byte MembershipTypeId { get; set; }
            public DateTime? Birthdate { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, (IdentityResult, Customer)>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<(IdentityResult, Customer)> Handle(Command request, CancellationToken cancellationToken)
            {
                var account = new Customer
                {
                    UserName =request.Email,
                    Email =request.Email,
                    Name =request.Name,
                    HasNewsletterSubscribed =request.HasNewsletterSubscribed,
                    MembershipTypeId =request.MembershipTypeId,
                    Birthdate =request.Birthdate,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(account, request.Password);

                return (result, account);
            }
        }
    }
}
