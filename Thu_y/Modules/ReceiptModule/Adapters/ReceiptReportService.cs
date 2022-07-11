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

        public ReceiptReportService(IServiceProvider serviceProvider)
        {
            _receiptReportRepository = serviceProvider.GetRequiredService<IReceiptReportRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public Task<string> CreateAsync(ReceiptReportModel model, CancellationToken cancellationToken = default)
        {
            var receipt = _mapper.Map<ReceiptReportEntity>(model);
            var data = _receiptReportRepository.Add(receipt);
            _unitOfWork.SaveChange();
            return Task.FromResult(data.Id);
        }

        public Task UpdateAsync(ReceiptModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var receipt = _receiptReportRepository.Get(x => x.Id == model.Id).FirstOrDefault();
                if (receipt == null) throw new Exception("No receipt found!") { HResult = 404 };

                _mapper.Map(model, receipt);
                _receiptReportRepository.Update(receipt);
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
                var checkReceipt = _receiptReportRepository.Get(w => w.Id == id && w.DateDeleted == null).FirstOrDefault();
                if (checkReceipt == null)
                {
                    throw new Exception("No user found!") { HResult = 400 };
                }
                _receiptReportRepository.Delete(checkReceipt);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public ICollection<ReceiptReportQuarantineModel> GetReceiptReport(ReceiptReportPagingModel model)
        {
            var data = _receiptReportRepository.Get(_ => _.UserId == model.UserId && _.ReceiptName == model.ReportName)
                                               .Skip(model.PageNumber * model.PageSize)
                                               .Take(model.PageSize).ToList();
            return _mapper.Map<ICollection<ReceiptReportQuarantineModel>>(data);
        }
    }
}
