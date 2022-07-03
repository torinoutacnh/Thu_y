using Microsoft.Extensions.DependencyInjection;
using Thu_y.Infrastructure.DbContext;

namespace Thu_y.Infrastructure.UOF
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
        Task<int> SaveChangeAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private bool disposed = false;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<IDbContext>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
