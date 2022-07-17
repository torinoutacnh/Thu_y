using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class RefreshTokenRepository : Repository<RefreshToken> , IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDbContext dbContext) : base(dbContext)
        { 
        }
        public void Delete(RefreshToken entity)
        {
            try
            {
                DbSet.Remove(entity);

            }
            catch (Exception)
            {
                DbContext.Entry(entity).Reload();
            }
        }
    }
}
