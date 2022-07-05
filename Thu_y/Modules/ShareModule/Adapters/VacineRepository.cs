using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ShareModule.Core;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ShareModule.Adapters
{
    public class VacineRepository : Repository<VacineEntity>, IVacineRepository
    {
        public VacineRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
