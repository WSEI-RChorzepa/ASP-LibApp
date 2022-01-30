using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Commands
{
    public class UpdateAccount
    {
        public class Command : IRequest<(IdentityResult Result, Customer Account)>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasNewsletterSubscribed { get; set; }
            public byte MembershipTypeId { get; set; }
            public DateTime? Birthdate { get; set; }
        }

        public class Handler : IRequestHandler<Command, (IdentityResult Result, Customer Customer)>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<(IdentityResult Result, Customer Customer)> Handle(Command request, CancellationToken cancellationToken)
            {
                var account = await _userManager.FindByIdAsync(request.Id.ToString());

                if (account == null)
                    throw new Exception($"Account with id: {request.Id} was not found.");

                account.Name = request.Name;
                account.HasNewsletterSubscribed = request.HasNewsletterSubscribed;
                account.MembershipTypeId = request.MembershipTypeId;
                account.Birthdate = request.Birthdate;

                var result = await _userManager.UpdateAsync(account);

                return (result, account);
            }
        }
    }
}
