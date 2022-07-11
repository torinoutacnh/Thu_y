using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;

namespace Thu_y.Modules.ReceiptModule.Ports
{
    public interface IReceiptService
    {
        bool CreateAsync(ReceiptModel model);
        Task UpdateAsync(ReceiptModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        bool AllocateReceipt(ReceiptAllocateModel model);
        bool UpdateAllocateReceipt(ReceiptAllocateModel model);
        bool DeleteAllocateReceipt(string id);
    }
}
