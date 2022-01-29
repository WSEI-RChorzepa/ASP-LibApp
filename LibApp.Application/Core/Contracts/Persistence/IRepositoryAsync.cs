using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibApp.Application.Core.Contracts.Persistence
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<List<T>> BrowseAsync();

        Task CreateAsync(T model);
        Task DeleteAsync(int id);

        Task<int> SaveChangesAsync();
    }
}
