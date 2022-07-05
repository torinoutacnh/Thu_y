using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class FormAttributeRepository : Repository<FormAttributeEntity>, IFormAttributeRepository
    {
        public FormAttributeRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
