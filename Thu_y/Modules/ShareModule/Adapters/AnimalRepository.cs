using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ShareModule.Core;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ShareModule.Adapters
{
    public class AnimalRepository : Repository<AnimalEntity>, IAnimalRepository
    {
        public AnimalRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
