using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserScheduleRepository : Repository<UserScheduleEntity>, IUserScheduleRepository
    {
        public UserScheduleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
