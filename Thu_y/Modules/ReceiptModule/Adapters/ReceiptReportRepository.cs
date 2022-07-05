using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptReportRepository : Repository<ReceiptReportEntity>, IReceiptReportRepository
    {
        public ReceiptReportRepository(IDbContext dbCOntext) :base(dbCOntext)
        {

        }
    }
}
