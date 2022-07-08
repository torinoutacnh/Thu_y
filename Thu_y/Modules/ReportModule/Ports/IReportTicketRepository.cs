using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportTicketRepository : IRepository<ReportTicketEntity>
    {
        ICollection<AnimalKillingReportModel> GetAnimalKillingReport(string userId);
    }
}
