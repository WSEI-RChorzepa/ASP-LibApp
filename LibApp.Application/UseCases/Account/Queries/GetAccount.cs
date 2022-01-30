using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Queries
{
    public class GetAccount
    {
        public class Query : IRequest<Customer>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Customer>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Customer> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userManager.FindByIdAsync(request.Id.ToString());
            }
        }
    }
}
