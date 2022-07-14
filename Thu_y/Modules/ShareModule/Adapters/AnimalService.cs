using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ShareModule.Core;
using Thu_y.Modules.ShareModule.Model;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ShareModule.Adapters
{
    //[RegisterClassAsScoped]
    public class AnimalService: IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnimalService(IServiceProvider serviceProvider)
        {
            _animalRepository = serviceProvider.GetRequiredService<IAnimalRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public Task<string> CreateAsync(AnimalModel model, CancellationToken cancellationToken = default)
        {
            var animal = _animalRepository.GetSingle(_ => _.Name == model.Name);
            if (animal != null) throw new Exception($"'{model.Name}' is existed!") { HResult = 400 };

            var entity = _mapper.Map<AnimalEntity>(model);
            var data = _animalRepository.Add(entity);
            _unitOfWork.SaveChange();

            return Task.FromResult(data.Id);
        }

        public Task UpdateAsync(AnimalModel model, CancellationToken cancellationToken = default)
        {
            var ani = _animalRepository.GetSingle(x => x.Id == model.Id);
            if (ani == null) throw new Exception("Not found animal!") { HResult = 404 };

            _mapper.Map(model, ani);
            _animalRepository.Update(ani);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }

        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var ani = _animalRepository.GetSingle(x => x.Id == id);
            if (ani == null) throw new Exception("Not found animal!") { HResult = 404 };

            _animalRepository.Delete(ani, true);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }
    }
}
