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

        public Task<string> CreateAsync(AbattoirModel model, CancellationToken cancellationToken = default)
        {
            var receipt = _mapper.Map<AbattoirEntity>(model);
            var data = _abattoirRepository.Add(receipt);
            _unitOfWork.SaveChange();
            return Task.FromResult(data.Id);
        }

        public Task UpdateAsync(AbattoirModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var receipt = GetByReceptId(model.Id);
                if (receipt == null) throw new Exception("No receipt found!") { HResult = 404 };

                _mapper.Map(model, receipt);
                _abattoirRepository.Update(receipt);
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
                _abattoirRepository.Delete(checkReceipt);
                _unitOfWork.SaveChange();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public AbattoirEntity GetByReceptId(string id)
        {
            var result =
               _abattoirRepository.Get(w => w.Id == id && w.DateDeleted == null).FirstOrDefault();
            return result;
        }
    }
}
