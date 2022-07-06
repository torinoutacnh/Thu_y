using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportService
    {
        bool CreateReport(ReportModel model);
        bool UpdateReport(ReportModel model);
        bool DeleteReport(string id);
    }
}
