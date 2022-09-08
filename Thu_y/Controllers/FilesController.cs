using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Controllers
{
    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IReportService _reportService;

        public FilesController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("excel")]
        public  IActionResult ExportExcel([FromBody] string userId)
        {
            var excel = _reportService.ExportExcel(userId);
            var ms = new MemoryStream();
            excel.SaveAs(ms);
            ms.Position = 0;
            return File(ms, "application/octet-stream", "reportExcel.xlsx");
        }

        [HttpPost("reports")]
        public IActionResult ExportReportToExcel()
        {
            var excel = _reportService.ExportReportToExcel();
            var ms = new MemoryStream();
            excel.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-thu-y.xlsx");
        }
    }
}
