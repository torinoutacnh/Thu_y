using System.Threading.Tasks;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReceiptService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _receiptRepository = serviceProvider.GetRequiredService<IReceiptRepository>();
        }

        public Task<string> CreateAsync(ReceiptEntity model , CancellationToken cancellationToken = default)
        {
            _receiptRepository.Add(model);
            _unitOfWork.SaveChange();
            return Task.FromResult(model.Id);
        }

        public Task UpdateAsync(ReceiptEntity model, CancellationToken cancellationToken = default)
        {
            try
            {
                _receiptRepository.Update(model);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    }
}
