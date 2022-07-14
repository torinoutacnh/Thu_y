using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class ListAnimalService : IListAnimalService
    {
        private readonly IListAnimalRepository _listAnimalRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportTicketRepository _reportTicketRepository;
        private readonly IAnimalRepository _animalRepository;

        public ListAnimalService(IServiceProvider serviceProvider)
        {
            _listAnimalRepository = serviceProvider.GetRequiredService<IListAnimalRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _reportTicketRepository = serviceProvider.GetRequiredService<IReportTicketRepository>();
            _animalRepository = serviceProvider.GetRequiredService<IAnimalRepository>();
        }

        #region Create ListAnimal
        public Task<string> CreateAsync(ListAnimalModel model, CancellationToken cancellationToken = default)
        {
            var report = _reportTicketRepository.GetSingle(_ => _.Id == model.ReportTicketId);
            if (report == null) throw new Exception("Not found report!") { HResult = 404 };
            var ani = _animalRepository.GetSingle(_ => _.Name == model.AnimalName);
            if (ani == null) throw new Exception($"Not found '{model.AnimalName}' in list!") { HResult = 404 };

            var entity = _mapper.Map<ListAnimalEntity>(model);
            entity.TotalPrice = entity.Amount * ani.Pricing;

            _listAnimalRepository.Add(entity);
            _unitOfWork.SaveChange();

            return Task.FromResult(entity.Id);
        }
        #endregion Create ListAnimal

        #region Delete ListAnimal
        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var checkReceipt = _listAnimalRepository.GetSingle(w => w.Id == id);
            if (checkReceipt == null)
                throw new Exception("Not found animal in list!") { HResult = 404 };

            _listAnimalRepository.Delete(checkReceipt, true);
            _unitOfWork.SaveChange();
            return Task.CompletedTask;
        }
        #endregion Delete ListAnimal;

        #region Get ListAnimal By ReportId
        public ICollection<ListAnimalModel> GetListAnimalByReportId(string reportId)
        {
            var seal = _listAnimalRepository.Get(_ => _.ReportTicketId == reportId).ToList();
            return _mapper.Map<ICollection<ListAnimalModel>>(seal);
        }
        #endregion Get ListAnimal By ReportId

        #region Update ListAnimal
        public Task UpdateAsync(UpdateListAnimalModel model, CancellationToken cancellationToken = default)
        {
            var report = _reportTicketRepository.GetSingle(_ => _.Id == model.ReportId);
            if (report == null) throw new Exception("Not found report!") { HResult = 404 };

            _listAnimalRepository.UpdateMultiListAnimal(model.ListAnimals, model.ReportId);

            return Task.CompletedTask;
        }
        #endregion Update ListAnimal
    }
}
