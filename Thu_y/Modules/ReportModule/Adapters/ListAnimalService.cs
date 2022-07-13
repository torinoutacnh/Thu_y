using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class ListAnimalService : IListAnimalService
    {
        private readonly IListAnimalRepository _listAnimalRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportTicketRepository _reportTicketRepository;

        public ListAnimalService(IServiceProvider serviceProvider)
        {
            _listAnimalRepository = serviceProvider.GetRequiredService<IListAnimalRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _reportTicketRepository = serviceProvider.GetRequiredService<IReportTicketRepository>();
        }


    }
}
