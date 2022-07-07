using AutoDependencyRegistration.Attributes;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.AbttoirModule.Core;
using Thu_y.Modules.AbttoirModule.Ports;

namespace Thu_y.Modules.AbttoirModule.Adapters
{
    //[RegisterClassAsScoped]
    public class AbattoirDetailRepository : Repository<AbattoirDetailEntity>, IAbattoirDetailRepository
    {
        public AbattoirDetailRepository(IDbContext dbContext) :  base(dbContext)
        {

        }
    }
}
