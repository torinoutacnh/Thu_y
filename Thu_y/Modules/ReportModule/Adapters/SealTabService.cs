using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class SealTabService : ISealTabService
    {
        private readonly ISealTabRepository _sealTabRepository;
        private readonly IReportTicketRepository _reportTicketRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISealConfigRepository _sealConfigRepository;
        public SealTabService(IServiceProvider serviceProvider)
        {
            _sealTabRepository = serviceProvider.GetRequiredService<ISealTabRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _reportTicketRepository = serviceProvider.GetRequiredService<IReportTicketRepository>();
            _sealConfigRepository = serviceProvider.GetRequiredService<ISealConfigRepository>();
        }

        #region Create SealTab
        public Task<string> CreateAsync(SealTabModel model, CancellationToken cancellationToken = default)
        {
            var report = _reportTicketRepository.GetSingle(_=>_.Id == model.ReportTicketId);
            if (report == null) throw new Exception("Not found report!") { HResult = 404 };
            var config = _sealConfigRepository.GetSingle(_ => _.SealName == model.SealName);
            if (config == null) throw new Exception("Not found seal config!") { HResult = 404 };

            var entity = _mapper.Map<SealTabEntity>(model);
            _sealTabRepository.Add(entity);
            _unitOfWork.SaveChange();

            return Task.FromResult(entity.Id);
        }
        #endregion Create SealTab

        #region Delete SealTab
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var checkReceipt = _sealTabRepository.GetSingle(w => w.Id == id);
            if (checkReceipt == null)
                throw new Exception("Not found Seal config!") { HResult = 404 };

            _sealTabRepository.Delete(checkReceipt, true);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Delete SealTab

        #region Get SealTab By ReportId
        public ICollection<SealTabModel> GetSealtabByReportId(string reportId)
        {
            var seal = _sealTabRepository.Get(_ => _.ReportTicketId == reportId).ToList();
            return _mapper.Map<ICollection<SealTabModel>>(seal);
        }
        #endregion Get SealTab By ReportId

        #region Update SealTab
        public Task UpdateAsync(UpdateSealTabModel model, CancellationToken cancellationToken = default)
        {
            var report = _reportTicketRepository.GetSingle(_ => _.Id == model.ReportId);
            if (report == null) throw new Exception("Not found report!") { HResult = 404 };

            _sealTabRepository.UpdateMultiSealTab(model.SealTabs, model.ReportId);

            return Task.CompletedTask;
        }
        #endregion Update SealTab
    }
}
