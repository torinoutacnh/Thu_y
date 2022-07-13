using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface ISealTabService
    {
        Task<string> CreateAsync(SealTabModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        ICollection<SealTabModel> GetSealtabByReportId(string reportId);
        Task UpdateAsync(UpdateSealTabModel model, CancellationToken cancellationToken = default);
    }
}
