using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;

namespace Thu_y.Modules.ReceiptModule.Ports
{
    public interface IReceiptService
    {
        Task<string> CreateAsync(ReceiptModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(ReceiptModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        bool AllocateReceipt(ReceiptAllocateModel model);
        bool UpdateAllocateReceipt(ReceiptAllocateModel model);
        bool DeleteAllocateReceipt(string id);
    }
}
