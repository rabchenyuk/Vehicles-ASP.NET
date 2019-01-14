using System.Threading.Tasks;
using Vincent.Core;

namespace Vincent.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VincentDbContext context;
        public UnitOfWork(VincentDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}