using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface ISealConfigService
    {
        Task CreateAsync(SealConfigModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(SealConfigModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        SealConfigModel GetSealConfigByIdOrName(string id, string sealName);
        ICollection<SealConfigModel> GetListSealConfig();
    }
}
