using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibApp.Persistence.Repositories
{
    public class BookRespository : RespositoryAsync<Book>, IBookRespository
    {
        public BookRespository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Book> GetAsync(int id)
        {
            return await Context.Books
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        public override async Task<List<Book>> BrowseAsync()
        {
            return await Context.Books
                .Include(a => a.Genre)
                .ToListAsync();
        }

    }
}
