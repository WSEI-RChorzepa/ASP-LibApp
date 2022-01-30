using LibApp.Application.Core.Models;
using LibApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibApp.Application.UseCases.Account.Commands
{
    public class AssignAccountToRoles
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
                if (request.SelectedItems.Any())
                {
                    var selectedRoles = request.SelectedItems
                        .Where(a => a.Selected)
                        .ToList();

                    foreach (var item in selectedRoles)
                        await _userManager.AddToRoleAsync(request.Account, item.Value);
                }

                return Unit.Value;
            }
        }
    }
}
