using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thu_y.Infrastructure.UOF;
using Thu_y.Infrastructure.Utils.Constant;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Modules.ShareModule.Ports;

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
        private readonly IAnimalRepository _animalRepository;
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

        #region Create ReportTicket
        public Task CreateReport(ReportModel model, CancellationToken cancellationToken = default)
        {
            switch (model.FormId)
            {
                case FormIdConstants.AnimalDeadId:
                    CreateAnimalDeadReport(model);
                    break;
                case FormIdConstants.QuanrantineId:
                    CreateQuarantineReport(model);
                    break;
                default:
                    throw new Exception($"Not found formId") { HResult = 404 };
            }
            return Task.CompletedTask;
        }
        private void CreateAnimalDeadReport(ReportModel model)
        {
            var entity = _mapper.Map<ReportTicketEntity>(model);
            decimal totalPrice = 0;

            foreach (var value in entity.Values)
            {
                value.ReportId = entity.Id;
            }

            if (entity.ListAnimals != null)
            {
                foreach (var animal in entity.ListAnimals)
                {
                    var unitPrice = _animalRepository.Get(_ => _.Id == animal.Id).Select(_ => _.Pricing).FirstOrDefault();
                    if (unitPrice == null)
                        throw new Exception($"Not found {animal.AnimalName} in animal categories") { HResult = 404 };

                    animal.TotalPrice = animal.Amount * unitPrice;
                    animal.ReportTicketId = entity.Id;
                    totalPrice += (decimal)animal.TotalPrice;
                }
            }
            entity.TotalPrice = totalPrice;
            _reportTicketRepository.Add(entity);
            _unitOfWork.SaveChange();
        }
        private void CreateQuarantineReport(ReportModel model)
        {
            var entity = _mapper.Map<ReportTicketEntity>(model);
            foreach (var value in entity.Values)
            {
                value.ReportId = entity.Id;
            }

            if (entity.SealTabs != null)
            {
                foreach (var seal in entity.SealTabs)
                {
                    seal.ReportTicketId = entity.Id;
                }
            }
            _reportTicketRepository.Add(entity);
            _unitOfWork.SaveChange();
        }
        #endregion Create ReportTicket


        public bool UpdateReport(ReportModel model)
        {
            var report = _reportTicketRepository.Get(x => x.Id ==model.Id);
            if (report == null) throw new Exception("No report found!") { HResult = 404 };

            //foreach(var val in report.Values)
            //{
            //    val.Value = model.Values?.Where(v => v.AttributeId == val.AttributeId).FirstOrDefault()?.Value;
            //    _reportTicketValueRepository.Update(val, val => val.Value);
            //}

            //_unitOfWork.SaveChange();

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
