using System.Threading.Tasks;

namespace LibApp.Persistence.Seed
{
    public interface IDataSeed
    {
        Task Initialize();
    }
}
