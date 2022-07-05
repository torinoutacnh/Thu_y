using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptRepository : Repository<ReceiptEntity>, IReceiptRepository
    {
        public ReceiptRepository(IDbContext dbContext) : base(dbContext)
        {

        }
    }
}
