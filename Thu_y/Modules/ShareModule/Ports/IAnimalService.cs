using Thu_y.Modules.ShareModule.Model;

namespace Thu_y.Modules.ShareModule.Ports
{
    public interface IAnimalService
    {
        Task<string> CreateAsync(AnimalModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(AnimalModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
