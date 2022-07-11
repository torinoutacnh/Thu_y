using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptReportPagingModel
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int PageNumber { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int PageSize { get; set; }

        public string UserId { get; set; }
        public string ReportName { get; set; }

        public ReceiptReportPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
