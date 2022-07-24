using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;

namespace Thu_y.Modules.ReceiptModule.Adapters
{
    public class ReceiptReportService : IReceiptReportService
    {
        private readonly IReceiptReportRepository _receiptReportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReceiptAllocateRepository _receiptAllocateRepository;

        public ReceiptReportService(IServiceProvider serviceProvider)
        {
            _receiptReportRepository = serviceProvider.GetRequiredService<IReceiptReportRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _receiptAllocateRepository = serviceProvider.GetRequiredService<IReceiptAllocateRepository>();
        }

        #region Create Receipt Report
        public Task<string> CreateAsync(ReceiptReportModel model, CancellationToken cancellationToken = default)
        {
            var allocateReceipt = _receiptAllocateRepository.GetSingle(_ => _.Id == model.ReceiptAllocateId);
            if (allocateReceipt == null) throw new Exception("Not found Allocate Receipt!") { HResult = 404 };

            var receipt = _mapper.Map<ReceiptReportEntity>(model);
            var data = _receiptReportRepository.Add(receipt);
            _unitOfWork.SaveChange();
            return Task.FromResult(receipt.Id);
        }
        #endregion Create Receipt Report

        #region Update Receipt Report
        public Task UpdateAsync(ReceiptReportModel model, CancellationToken cancellationToken = default)
        {
            var receipt = _receiptReportRepository.GetSingle(x => x.Id == model.Id);
            if (receipt == null) throw new Exception("Not found Receipt Report!") { HResult = 404 };

            _mapper.Map(model, receipt);
            _receiptReportRepository.Update(receipt);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Update Receipt Report

        #region Delete Receipt Report
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var checkReceipt = _receiptReportRepository.GetSingle(w => w.Id == id);
            if (checkReceipt == null)
                throw new Exception("Not found Receipt Report!") { HResult = 404 };

            _receiptReportRepository.Delete(checkReceipt);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Delete Receipt Report

        #region Get Receipt Report
        public ICollection<ReceiptReportQuarantineModel> GetReceiptReport(ReceiptReportPagingModel model, string userId = null)
        {
            var data = userId switch
            {
                null => _receiptReportRepository.Get(_ => model.AllocateId == null ? true : _.ReceiptAllocateId == model.AllocateId)
                                               .Skip(model.PageNumber * model.PageSize)
                                               .Take(model.PageSize).ToList(),
                _ => _receiptReportRepository.Get(_ => _.UserId == userId &&
                                                       model.AllocateId == null ? true : _.ReceiptAllocateId == model.AllocateId)
                                               .Skip(model.PageNumber * model.PageSize)
                                               .Take(model.PageSize).ToList()
            };
            return _mapper.Map<ICollection<ReceiptReportQuarantineModel>>(data);
        }
        #endregion Get Receipt Report
    }
}
