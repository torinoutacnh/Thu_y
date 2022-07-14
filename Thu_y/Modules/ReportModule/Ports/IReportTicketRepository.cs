using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IReportTicketRepository : IRepository<ReportTicketEntity>
    {
        /// <summary>
        /// Danh sách báo cáo giết mổ (new)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        ICollection<AnimalKillingReportModel> GetAnimalKillingReport(string userId);

        /// <summary>
        /// Danh sách giết mổ (old)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ICollection<ListAbttoirReportModel> GetListAbattoirReport(string userId);

        /// <summary>
        /// Danh sách báo cáo kiểm dịch
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ICollection<ListQuarantineReportModel> GetListQuarantineReport(string userId);

        /// <summary>
        /// Báo cáo doanh thu kiểm dịch
        /// </summary>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        ICollection<QuarantineRevenueReport> GetQuarantineRevenueReport(DateTimeOffset fromDay, DateTimeOffset toDay);

        /// <summary>
        /// Update report with multi attribute
        /// </summary>
        /// <param name="lsModel"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        bool UpdateMultiReport(List<Values> lsModel, string reportId);
    }
}
