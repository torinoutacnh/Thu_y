using AutoMapper;
using System.Data.Entity;
using System.Threading.Tasks;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptAllocateRepository _receiptAllocateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ReceiptService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _receiptAllocateRepository = serviceProvider.GetRequiredService<IReceiptAllocateRepository>();
            _receiptRepository = serviceProvider.GetRequiredService<IReceiptRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public bool CreateAsync(ReceiptModel model)
        {
            var entity = new ReceiptEntity();
            _mapper.Map(model, entity);
            //foreach (var item in enity.Allocates)
            //{
            //    item.ReceiptId = enity.Id;
            //}
            entity.Allocates.All(_ => { _.ReceiptId = entity.Id; return true; });
            _receiptRepository.Add(entity);
            _unitOfWork.SaveChange();
            return true;
        }

        public Task UpdateAsync(ReceiptModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var receipt = _receiptRepository.Get(x => x.Id == model.Id).FirstOrDefault();
                if (receipt == null) throw new Exception("No receipt found!") { HResult = 404 };

                _mapper.Map(model,receipt);
                _receiptRepository.Update(receipt);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkReceipt = GetByReceptId(id);
                if (checkReceipt == null)
                {
                    throw new Exception("No user found!") { HResult = 400 };
                }
                _receiptRepository.Delete(checkReceipt);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
        
        public ReceiptEntity GetByReceptId(string id)
        {
            var result =
               _receiptRepository.Get(w => w.Id == id && w.DateDeleted == null).FirstOrDefault();
            return result;
        }

        public bool AllocateReceipt(ReceiptAllocateModel model)
        {
            var item = _mapper.Map<ReceiptAllocateEntity>(model);
            _receiptAllocateRepository.Add(item);
            _unitOfWork.SaveChange();
            return true;
        }

        public bool UpdateAllocateReceipt(ReceiptAllocateModel model)
        {
            var item = _receiptAllocateRepository.Get(x => x.Id == model.Id).FirstOrDefault();
            if (item == null) throw new Exception("Not found!") { HResult = 400 };

            _mapper.Map(model, item);
            _receiptAllocateRepository.Update(item);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteAllocateReceipt(string id)
        {
            var item = _receiptAllocateRepository.Get(x => x.Id == id).FirstOrDefault();
            if (item == null) throw new Exception("Not found!") { HResult = 400 };

            _receiptAllocateRepository.Delete(item);
            _unitOfWork.SaveChange();

            return true;
        }
    }
}
