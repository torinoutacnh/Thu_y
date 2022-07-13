using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.AbttoirModule.Core;
using Thu_y.Modules.AbttoirModule.Model;
using Thu_y.Modules.AbttoirModule.Ports;

namespace Thu_y.Modules.AbttoirModule.Adapters
{
    //[RegisterClassAsScoped]
    public class AbattoirService : IAbattoirService
    {
        private readonly IAbattoirRepository _abattoirRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AbattoirService(IServiceProvider serviceProvider)
        {
            _abattoirRepository = serviceProvider.GetRequiredService<IAbattoirRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        #region Create Abattoir
        public Task<string> CreateAsync(AbattoirModel model, CancellationToken cancellationToken = default)
        {
            var abattoir = _mapper.Map<AbattoirEntity>(model);
            var data = _abattoirRepository.Add(abattoir);
            _unitOfWork.SaveChange();
            return Task.FromResult(data.Id);
        }
        #endregion Create Abattoir

        #region Update Abattoir
        public Task UpdateAsync(AbattoirModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var abattoir = GetAbattoirById(model.Id);
                if (abattoir == null) throw new Exception("No receipt found!") { HResult = 404 };

                _mapper.Map(model, abattoir);
                _abattoirRepository.Update(abattoir);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
        #endregion Update Abattoir

        #region Delete Abattoir
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkReceipt = GetAbattoirById(id);
                if (checkReceipt == null)
                {
                    throw new Exception("No abattoir found!") { HResult = 400 };
                }
                _abattoirRepository.Delete(checkReceipt);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
        #endregion Delete Abattoir

        #region Get Abattoir by Id
        public AbattoirEntity GetAbattoirById(string id)
        {
            return _abattoirRepository.Get(w => w.Id == id).FirstOrDefault();

        }
        #endregion Get Abattoir by Id

    }
}
