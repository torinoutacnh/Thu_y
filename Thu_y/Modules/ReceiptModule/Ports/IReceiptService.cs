using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;

namespace Thu_y.Modules.ReceiptModule.Ports
{
    public interface IReceiptService
    {
        Task CreateAsync(ReceiptModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateReceiptModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task CreateAllocateReceipt(ReceiptAllocateModel model, CancellationToken cancellationToken = default);
        Task UpdateAllocateReceipt(ReceiptAllocateModel model, CancellationToken cancellationToken = default);
        Task DeleteAllocateReceipt(string id, CancellationToken cancellationToken = default);
    }
}
