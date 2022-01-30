using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Queries
{
    public class GetRoles
    {
        public class Query : IRequest<List<IdentityRole<int>>>
        {

        }

        public class Handler : IRequestHandler<Query, List<IdentityRole<int>>>
        {
            private readonly RoleManager<IdentityRole<int>> _roleManager;

            public Handler(RoleManager<IdentityRole<int>> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<List<IdentityRole<int>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await _roleManager.Roles.ToListAsync();

                return roles;
            }
        }
    }
}
