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

        public string AllocateId { get; set; }

        public ReceiptReportPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
