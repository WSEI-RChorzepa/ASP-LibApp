using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Queries
{
    public static class GetRolesByAccount
    {
        public class Query : IRequest<IList<string>>
        {
            public Customer Account { get; set; }
        }

        public class Handler : IRequestHandler<Query, IList<string>>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IList<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userManager.GetRolesAsync(request.Account);
            }
        }
    }
}
