using Thu_y.Modules.ReceiptModule.Model;

namespace Thu_y.Modules.ReceiptModule.Ports
{
    public interface IReceiptReportService
    {
        Task<string> CreateAsync(ReceiptReportModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(ReceiptModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        ICollection<ReceiptReportQuarantineModel> GetReceiptReport(ReceiptReportPagingModel model);
    }
}
