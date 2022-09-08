using AutoMapper;
using ClosedXML.Excel;
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
        public Task<string> CreateReport(ReportModel model, CancellationToken cancellationToken = default)
        {
            //var check = _reportTicketRepository.Get(x=>x.FormId==model.FormId && x.SerialNumber==model.SerialNumber).Any();
            //if (check) throw new Exception($"Biên bản với số {model.SerialNumber} đã tồn tại!") { HResult = 400 };
            switch (model.FormId)
            {
                case FormIdConstants.AnimalDeadId:
                    return Task.FromResult(CreateAnimalDeadReport(model));
                case FormIdConstants.QuanrantineId:
                    return Task.FromResult(CreateQuarantineReport(model));
                default:
                    return Task.FromResult(CreateDefaultReport(model));
            }
        }
        private string CreateAnimalDeadReport(ReportModel model)
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

            return entity.Id;
        }
        private string CreateQuarantineReport(ReportModel model)
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
            return entity.Id;
        }

        private string CreateDefaultReport(ReportModel model)
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
            return entity.Id;
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

        public XLWorkbook ExportReportToExcel()
        {
            //var data = _reportTicketRepository.GetListQuarantineReport(userId);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var excel = new XLWorkbook();

            var ws = excel.Worksheets.Add("VE NIEM PHONG");
            if(ws != null)
            {
                ws.Cell("A2").Value = "BÁO CÁO SỬ DỤNG VÉ NIÊM PHONG NĂM 2022";
                CreateExcelLayout(ws);
            }
            ws = excel.Worksheets.Add("VE TIEU DOC");
            if (ws != null)
            {
                ws.Cell("A2").Value = "BÁO CÁO SỬ DỤNG VÉ TIÊU ĐỘC NĂM 2022";
                CreateExcelLayout(ws);
            }

            return excel;
        }

        private void CreateExcelLayout(IXLWorksheet ws)
        {
            #region Header
            ws.Cell("B1").Value = "PHÒNG KIỂM DỊCH";
            ws.Cell("N1").Value = "Tháng 7";
            ws.Cell("E3").Value = "Thông tin tồn đầu kỳ";
            ws.Cell("H3").Value = "Thông tin đã sử dụng trong kỳ";
            ws.Cell("L3").Value = "Thông tin tồn cuối kỳ";

            //Merge rows
            foreach (var str in new[] { "B1:E2", "A2:O3", "N1:O2", "E3:G4", "H3:J4", "L3:N3" })
                ws.Range(str).Row(1).Merge();

            //Assign values for headercell
            string[] titles = { "TT", "Kí hiệu", "Quyển sổ", "Mệnh giá", "Số tờ (tờ)", "Từ số seri", "Đến số seri", "Tổng (tờ)", "Số tờ dùng", "Số tờ hủy", "Số tiền thu (đ)", "Số tờ (tờ)", "Từ số seri", "Đến số seri", "Ghi chú" };
            for (int i = 1; i <= titles.Length; i++)
            {
                if (i >= 1 && i <= 4 || i == 11 || i == 15)
                    ws.Cell(3, i).Value = titles[i - 1];
                ws.Cell(4, i).Value = titles[i - 1];
            }
            //Merge columns
            ws.Range("A3:O4").Columns("A:D,K,O").ToList().ForEach(col =>
            {
                col.Merge();
                col.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            });

            //styling
            var header = ws.Cells("A1:O4").Style;
            header.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            header.Font.Bold = true;
            header.Font.FontName = "arial";
            var tableHeader = ws.Cells("A3:O4").Style;
            tableHeader.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tableHeader.Border.TopBorder = XLBorderStyleValues.Medium;
            tableHeader.Border.BottomBorder = XLBorderStyleValues.Medium;
            ws.Columns().AdjustToContents();
            #endregion Header

            int currentRow = 5;

            //insert data


            #region Footer
            currentRow = ws.RowsUsed().Count() + 1;
            ws.Cell(currentRow, 2).Value = "TỔNG";
            var tableFooter = ws.Range($"B{currentRow}:D{currentRow + 1}").Row(1);
            tableFooter.Merge();
            tableFooter.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            tableFooter.Style.Font.Bold = true;

            ws.Cells($"A{currentRow}:O{currentRow}").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            foreach (var a in new[] { "E", "H", "I", "J", "K", "L" }) ws.Cell($"{a}{currentRow}").FormulaA1 = $"=SUM({a}5:{a}{currentRow-1})";
            #endregion Footer
        }
    }
}
