using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ShareModule.Core;
using Thu_y.Modules.ShareModule.Model;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ShareModule.Adapters
{
    public class AnimalService: IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnimalService(IServiceProvider serviceProvider)
        {
            _animalRepository = serviceProvider.GetRequiredService<IAnimalRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<Mapper>();
        }

        public bool CreateAnimal(AnimalModel model)
        {
            var ani = _mapper.Map<AnimalEntity>(model);
            _animalRepository.Add(ani);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool UpdateAnimal(AnimalModel model)
        {
            var ani = _animalRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            if (ani == null) throw new Exception("No animal found!") { HResult = 400 };

            _mapper.Map(model, ani);
            _animalRepository.Update(ani);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteAnimal(string id)
        {
            var ani = _animalRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (ani == null) throw new Exception("No animal found!") { HResult = 400 };

            _animalRepository.Delete(ani);
            _unitOfWork.SaveChange();

            return true;
        }
    }
}
