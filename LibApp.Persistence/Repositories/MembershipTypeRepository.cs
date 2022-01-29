using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Persistence.Repositories
{
    public class MembershipTypeRepository : RespositoryAsync<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<MembershipType> GetAsync(int id)
        {
            var generes = await Context.MembershipTypes.ToListAsync();
            return generes.FirstOrDefault(a => a.Id == id);
        }
    }
}
