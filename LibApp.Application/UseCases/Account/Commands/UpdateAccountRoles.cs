using LibApp.Application.Core.Models;
using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Commands
{
    public class UpdateAccountRoles 
    {
        public class Command : IRequest
        {
            public Customer Account { get; set; }
            public List<SelectedItem> SelectedItems { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<Customer> _userManager;

            public Handler(UserManager<Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userRoles = await _userManager.GetRolesAsync(request.Account);

                var selectedRoles = request.SelectedItems
                    .Where(a => a.Selected)
                    .Select(a => a.Value)
                    .ToList();

                var unSelectedRoles = request.SelectedItems
                    .Where(a => !a.Selected)
                    .Select(a => a.Value)
                    .ToList();

                foreach (var role in request.SelectedItems)
                {
                    if (role.Selected)
                    {
                        if (!userRoles.Contains(role.Value))
                            await _userManager.AddToRoleAsync(request.Account, role.Value);
                    }
                    else
                    {
                        if (userRoles.Contains(role.Value))
                            await _userManager.RemoveFromRoleAsync(request.Account, role.Value);
                    }
                }

                return Unit.Value;
            }
        }
    }
}
