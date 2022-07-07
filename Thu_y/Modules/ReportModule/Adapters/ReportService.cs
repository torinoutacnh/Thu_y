using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class ReportService: IReportService
    {
        private readonly IReportTicketRepository _reportTicketRepository;
        private readonly IReportTicketValueRepository _reportTicketValueRepository;
        private readonly IFormService _formService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IListAnimalRepository _listAnimalRepository;
        private readonly ISealTabRepository _sealTabRepository;
        public ReportService(IServiceProvider serviceProvider)
        {
            _reportTicketRepository = serviceProvider.GetRequiredService<IReportTicketRepository>();
            _reportTicketValueRepository = serviceProvider.GetRequiredService<IReportTicketValueRepository>();
            _formService = serviceProvider.GetRequiredService<IFormService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _sealTabRepository = serviceProvider.GetRequiredService<ISealTabRepository>();
            _listAnimalRepository = serviceProvider.GetRequiredService<IListAnimalRepository>();
        }

        public bool CreateReport(ReportModel model)
        {
            var report = new ReportTicketEntity();
            _mapper.Map(model,report);
            report.Values.All(x =>
            {
                x.ReportId = report.Id;
                return true;
            });

            _reportTicketRepository.Add(report);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool UpdateReport(ReportModel model)
        {
            var report = _reportTicketRepository.Get(x => x.Id.Equals(model.Id)).Include(x => x.Values).FirstOrDefault();
            if (report == null) throw new Exception("No report found!") { HResult = 404 };

            var newVals = _mapper.ProjectTo<ReportTicketValueEntity>(model.Values.AsQueryable());
            _mapper.Map(model, report);
            _reportTicketRepository.Update(report);

            report.Values.All(x =>
            {
                _reportTicketValueRepository.Delete(x, true);
                return true;
            });

            _reportTicketValueRepository.AddRange(newVals.ToArray());
            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteReport(string id)
        {
            var report = _reportTicketRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (report == null) throw new Exception("No report found!") { HResult = 404 };

            _reportTicketRepository.Delete(report);
            _unitOfWork.SaveChange();

            return true;
        }
    }
}
