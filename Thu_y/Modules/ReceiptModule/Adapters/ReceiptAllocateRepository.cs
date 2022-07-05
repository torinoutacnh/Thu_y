using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptAllocateRepository : Repository<ReceiptAllocateEntity>, IReceiptAllocateRepository
    {
        public ReceiptAllocateRepository(IDbContext dbContext): base(dbContext)
        {

        }
    }
}
