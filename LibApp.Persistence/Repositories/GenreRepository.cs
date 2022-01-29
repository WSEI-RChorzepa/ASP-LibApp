using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Persistence.Repositories
{
    public class GenreRepository : RespositoryAsync<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Genre> GetAsync(int id)
        {
            var generes = await Context.Genre.ToListAsync();
            return generes.FirstOrDefault(a=>a.Id == id);
        }
    }
}
