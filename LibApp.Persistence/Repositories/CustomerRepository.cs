using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibApp.Persistence.Repositories
{
    public class CustomerRepository : RespositoryAsync<Customer>, ICustomerRepository
    {

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<Customer> GetAsync(int id)
        {
            return Context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefaultAsync(a=>a.Id.Equals(id));
        }

        public override Task<List<Customer>> BrowseAsync()
        {
            return Context.Customers
                .Include(c => c.MembershipType)
                .ToListAsync();
        }
    }
}
