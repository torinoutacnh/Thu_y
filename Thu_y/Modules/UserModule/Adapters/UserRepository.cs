using System.Linq.Expressions;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public UserEntity GetSingle(Expression<Func<UserEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

    }
}
