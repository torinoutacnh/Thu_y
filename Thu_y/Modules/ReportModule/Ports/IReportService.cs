using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportService
    {
        Task CreateReport(ReportModel model, CancellationToken cancellationToken = default);
        bool UpdateReport(ReportModel model);
        bool DeleteReport(string id);
    }
}
