using System.Linq.Expressions;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetSingle(Expression<Func<UserEntity, bool>> predicate);
        UserEntity GetByVerifyToken(string token);
        bool Edit(UserEntity entity);
    }
}
