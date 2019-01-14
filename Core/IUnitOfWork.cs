using System.Threading.Tasks;

namespace Vincent.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}