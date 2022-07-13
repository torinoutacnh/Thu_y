using AutoMapper;
using Thu_y.Infrastructure.Repository;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class SealConfigService : ISealConfigService
    {
        private readonly ISealConfigRepository _sealConfigRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SealConfigService(IServiceProvider serviceProvider) 
        {
            _sealConfigRepository = serviceProvider.GetRequiredService<ISealConfigRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        #region Create Seal Config
        public Task CreateAsync(SealConfigModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<SealConfigEntity>(model);

            _sealConfigRepository.Add(entity);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }
        #endregion Create Seal Config

        #region Update Seal Config
        public Task UpdateAsync(SealConfigModel model, CancellationToken cancellationToken = default)
        {
            var seal = _sealConfigRepository.GetSingle(x => x.Id == model.Id);
            if (seal == null) throw new Exception("Not found seal config!") { HResult = 404 };

            _mapper.Map(model, seal);
            _sealConfigRepository.Update(seal);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Update Seal Config

        #region Delete Seal Config
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var checkReceipt = _sealConfigRepository.GetSingle(w => w.Id == id);
            if (checkReceipt == null)
                throw new Exception("Not found Seal config!") { HResult = 404 };

            _sealConfigRepository.Delete(checkReceipt, true);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Delete Seal Config

        #region Get SealConfig By Id or Name
        public SealConfigModel GetSealConfigByIdOrName(string id, string sealName)
        {
            var entity = new SealConfigEntity();
            if (string.IsNullOrEmpty(id))
                entity = _sealConfigRepository.GetSingle(_ => _.SealName == sealName);
            else
                entity = _sealConfigRepository.GetSingle(_ => _.Id == id);
            return _mapper.Map<SealConfigModel>(entity);
        }
        #endregion Get SealConfig By Id or Name
    }
}
