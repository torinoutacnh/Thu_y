using Thu_y.Modules.AbttoirModule.Core;
using Thu_y.Modules.AbttoirModule.Model;

namespace Thu_y.Modules.AbttoirModule.Ports
{
    public interface IAbattoirService
    {
        Task<string> CreateAsync(AbattoirModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(AbattoirModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        AbattoirEntity GetByReceptId(string id);
    }
}
