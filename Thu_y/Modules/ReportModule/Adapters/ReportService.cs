using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
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
            _animalRepository = serviceProvider.GetRequiredService<IAnimalRepository>();
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
                    CreateDefaultReport(model);
                    break;
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
                    //var unitPrice = _animalRepository.Get(_ => _.Id == animal.Id).Select(_ => _.Pricing).FirstOrDefault();
                    //if (unitPrice == null)
                    //    throw new Exception($"Not found {animal.AnimalName} in animal categories") { HResult = 404 };
                    var lsAnimal = _animalRepository.Get();
                    var unitPrice = lsAnimal.Where(_ => _.Name == animal.AnimalName).Select(x => x.Pricing).FirstOrDefault();

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

            if (entity.ListAnimals != null)
            {
                foreach (var animal in entity.ListAnimals)
                {
                    var lsAnimal = _animalRepository.Get();
                    var unitPrice = lsAnimal.Where(_ => _.Name == animal.AnimalName).Select(x => x.Pricing).FirstOrDefault();

                    if (unitPrice == null)
                        throw new Exception($"Not found {animal.AnimalName} in animal categories") { HResult = 404 };

                    animal.TotalPrice = animal.Amount * unitPrice;
                    animal.ReportTicketId = entity.Id;
                }
            }

            if (entity.SealTabs != null)
            {
                foreach (var seal in entity.SealTabs)
                {
                    seal.ReportTicketId = entity.Id;
                    entity.TotalPrice += 1; // chạy hết vong thì được tổng số seal
                }
            }

            _reportTicketRepository.Add(entity);
            _unitOfWork.SaveChange();
        }

        private void CreateDefaultReport(ReportModel model)
        {
            var entity = _mapper.Map<ReportTicketEntity>(model);
            if (entity.Values != null)
            {
                foreach (var value in entity.Values)
                {
                    value.ReportId = entity.Id;
                }
            }

            if (entity.ListAnimals != null)
            {
                foreach (var animal in entity.ListAnimals)
                {
                    var lsAnimal = _animalRepository.Get();
                    var unitPrice = lsAnimal.Where(_ => _.Name == animal.AnimalName).Select(x => x.Pricing).FirstOrDefault();

                    if (unitPrice == null)
                        throw new Exception($"Not found {animal.AnimalName} in animal categories") { HResult = 404 };

                    animal.TotalPrice = animal.Amount * unitPrice;
                    animal.ReportTicketId = entity.Id;
                }
            }

            if (entity.SealTabs != null)
            {
                foreach (var seal in entity.SealTabs)
                {
                    seal.ReportTicketId = entity.Id;
                    entity.TotalPrice += 1; // chạy hết vong thì được tổng số seal
                }
            }
            _reportTicketRepository.Add(entity);
            _unitOfWork.SaveChange();
        }
        #endregion Create ReportTicket

        #region Update Report
        public Task UpdateReport(UpdateReportModel model)
        {
            var report = _reportTicketRepository.GetSingle(x => x.Id == model.ReportId);
            if (report == null) throw new Exception("No report found!") { HResult = 404 };

            _reportTicketRepository.UpdateMultiReport(model.Values, model.ReportId);
            return Task.CompletedTask;
        }
        #endregion Update Report

        #region Delete Report
        public Task DeleteReport(string id)
        {
            var report = _reportTicketRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (report == null) throw new Exception("No report found!") { HResult = 404 };

            _reportTicketRepository.Delete(report);
            _unitOfWork.SaveChange();

            return Task.CompletedTask;
        }
        #endregion Delete Report

        #region Get Report
        public ICollection<ReportModel> GetReport(ReportPagingModel model, string userId, bool isManager = false)
        {
            var reports = new List<ReportTicketEntity>();
            if (isManager)
            {
                reports = _reportTicketRepository.Get(x => x.FormId == model.FormId, false, y => y.Values)
                                                  .OrderByDescending(x => x.DateCreated)
                                                  .Skip(model.PageNumber * model.PageSize)
                                                  .Take(model.PageSize).ToList();
                return _mapper.Map<ICollection<ReportModel>>(reports);
            }

            reports = _reportTicketRepository.Get(x => x.UserId == userId && x.FormId == model.FormId, false, y => y.Values)
                                              .OrderByDescending(x => x.DateCreated)
                                              .Skip(model.PageNumber * model.PageSize)
                                              .Take(model.PageSize).ToList();
            return _mapper.Map<ICollection<ReportModel>>(reports);
        }
        #endregion Get Report

        public ExcelPackage ExportExcel(string userId)
        {
            var data = _reportTicketRepository.GetListQuarantineReport(userId);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var excel = new ExcelPackage();

            var WookSheet = excel.Workbook.Worksheets.Add("Report");
            WookSheet.Cells[2,1].LoadFromCollection(data, true, TableStyles.Dark9);
            var modelTable = WookSheet.Cells[$"A1:E{data.Count + 1}"];
            WookSheet.Cells["A1:E" + (data.Count + 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            WookSheet.Cells.AutoFitColumns();

            return excel;
        }
    }
}
