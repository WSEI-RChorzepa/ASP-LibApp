using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Commands
{
    public class RemoveAccount
    {
        public class Command : IRequest<IdentityResult>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, IdentityResult>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var account = await _userManager.FindByIdAsync(request.Id.ToString());

                if(account == null)
                    throw new Exception($"Account with id: {request.Id} was not found.");

                return await _userManager.DeleteAsync(account);
            }
        }
    }
}
