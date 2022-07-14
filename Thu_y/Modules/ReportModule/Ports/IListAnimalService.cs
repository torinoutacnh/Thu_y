using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IListAnimalService
    {
        Task<string> CreateAsync(ListAnimalModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        ICollection<ListAnimalModel> GetListAnimalByReportId(string reportId);
        Task UpdateAsync(UpdateListAnimalModel model, CancellationToken cancellationToken = default);
    }
}
