using AutoMapper;
using System.Data.Entity;
using System.Threading.Tasks;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptAllocateRepository _receiptAllocateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReceiptReportRepository _receiptReportRepository;
        public ReceiptService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _receiptAllocateRepository = serviceProvider.GetRequiredService<IReceiptAllocateRepository>();
            _receiptRepository = serviceProvider.GetRequiredService<IReceiptRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _receiptReportRepository = serviceProvider.GetRequiredService<IReceiptReportRepository>();
        }

        #region Get Receipt by Id
        public ReceiptEntity GetReceptById(string id)
        {
            var result =
               _receiptRepository.GetSingle(w => w.Id == id && w.DateDeleted == null);
            return result;
        }
        #endregion Get Receipt by Id

        #region Create Receipt
        public Task CreateAsync(ReceiptModel model, CancellationToken cancellationToken = default)
        {
            var entity = new ReceiptEntity();
            _mapper.Map(model, entity);

            foreach (var item in entity.Allocates)
            {
                item.ReceiptId = entity.Id;
                item.TotalPage = item.Amount * entity.Page;
            }


            _receiptRepository.Add(entity);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Create Receipt

        #region Update Receipt
        public Task UpdateAsync(UpdateReceiptModel model, CancellationToken cancellationToken = default)
        {

            var receipt = GetReceptById(model.Id);
            if (receipt == null) throw new Exception("No receipt found!") { HResult = 404 };

            if (model.Page != receipt.Page)
            {
                var allocate = _receiptAllocateRepository.Get(_ => _.ReceiptId == receipt.Id).ToList();
                foreach (var item in allocate)
                {
                    item.ReceiptId = receipt.Id;
                    item.TotalPage = item.Amount * receipt.Page;
                    _receiptAllocateRepository.Update(item);
                }
            }

            _mapper.Map(model, receipt);
            _receiptRepository.Update(receipt);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;

        }
        #endregion Update Receipt

        #region Delete Receipt
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var receipt = _receiptRepository.Get(_ => _.Id == id, true, _ => _.Allocates).FirstOrDefault();
            if (receipt == null)
                throw new Exception("No Receipt found!") { HResult = 400 };

            receipt.DateDeleted = SystemHelper.SystemTimeNow;

            if (receipt.Allocates != null)
            {
                foreach (var allocate in receipt.Allocates)
                {
                    allocate.DateDeleted = SystemHelper.SystemTimeNow;
                    _receiptAllocateRepository.Update(allocate);
                }
            }
            _receiptRepository.Update(receipt);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Delete Receipt

        #region Create Allocate Receipt
        public Task CreateAllocateReceipt(ReceiptAllocateModel model, CancellationToken cancellationToken = default)
        {
            var receipt = GetReceptById(model.ReceiptId);
            if (receipt == null)
                throw new Exception("Not found Receipt!") { HResult = 404 };

            var totalPage = receipt.Page * model.Amount;
            var entity = _mapper.Map<ReceiptAllocateEntity>(model);
            entity.TotalPage = totalPage;
            entity.RemainPage = totalPage;

            _receiptAllocateRepository.Add(entity);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Create Allocate Receipt

        #region Update Allocate Receipt
        public Task UpdateAllocateReceipt(ReceiptAllocateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _receiptAllocateRepository.GetSingle(x => x.Id == model.Id);
            if (entity == null) throw new Exception("Not found AllocateReceipt!") { HResult = 400 };

            var receipt = GetReceptById(entity.ReceiptId);
            if (receipt == null) throw new Exception("Not found Receipt!") { HResult = 400 };

            _mapper.Map(model, entity);
            entity.TotalPage = receipt.Page * model.Amount;

            var pageUse = _receiptReportRepository.Get(_ => _.ReceiptAllocateId == entity.Id)?
                                                  .Sum(x => x.PageUse).Value?? 0;
            entity.RemainPage = entity.TotalPage - pageUse;
            _receiptAllocateRepository.Update(entity);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }
        #endregion Update Allocate Receipt

        #region Delete Allocate Receipt
        public Task DeleteAllocateReceipt(string id, CancellationToken cancellationToken = default)
        {
            var item = _receiptAllocateRepository.GetSingle(x => x.Id == id);
            if (item == null) throw new Exception("Not found AllocateReceipt!") { HResult = 400 };

            _receiptAllocateRepository.Delete(item);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }
        #endregion Delete Allocate Receipt
    }
}
