﻿using ClosedXML.Excel;
using OfficeOpenXml;
using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportService
    {
        Task<string> CreateReport(ReportModel model, CancellationToken cancellationToken = default);
        Task UpdateReport(UpdateReportModel model);
        Task DeleteReport(string id);
        ExcelPackage ExportExcel(string userId);
        XLWorkbook ExportReportToExcel();
        ICollection<ReportModel> GetReport(ReportPagingModel model, string userId, bool isManager = false);
        ExcelPackage ExportAimalDailyExcel();
    }
}
