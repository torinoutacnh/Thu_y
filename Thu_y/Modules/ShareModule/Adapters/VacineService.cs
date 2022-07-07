using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ShareModule.Ports;

namespace Thu_y.Modules.ShareModule.Adapters
{
    //[RegisterClassAsScoped]
    public class VacineService : IVacineService
    {
        private readonly IVacineRepository _vacineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacineService(IServiceProvider serviceProvider)
        {
            _vacineRepository = serviceProvider.GetRequiredService<IVacineRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }


    }
}
