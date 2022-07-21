using OfficeOpenXml;
using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportService
    {
        Task CreateReport(ReportModel model, CancellationToken cancellationToken = default);
        Task UpdateReport(UpdateReportModel model);
        Task DeleteReport(string id);
        ExcelPackage ExportExcel(string userId);
    }
}
