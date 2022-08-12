using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportTicketValueRepository : IRepository<ReportTicketValueEntity>
    {
        bool DeleteByAttributeId(string attributeId);
    }
}
