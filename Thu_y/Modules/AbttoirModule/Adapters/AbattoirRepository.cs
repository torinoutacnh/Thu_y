using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.AbttoirModule.Core;
using Thu_y.Modules.AbttoirModule.Ports;

namespace Thu_y.Modules.AbttoirModule.Adapters
{
    public class AbattoirRepository : Repository<AbattoirEntity>, IAbattoirRepository
    {
        public AbattoirRepository(IDbContext dbContext) : base(dbContext)
        {

        }
    }
}
