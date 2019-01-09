using System.Threading.Tasks;

namespace Vincent.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}