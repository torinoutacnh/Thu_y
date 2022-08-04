using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportPagingModel
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int PageNumber { get; set; }
        [Range(0, int.MaxValue)]
        [Required]
        public int PageSize { get; set; }
        public string? FormId { get; set; }
        //public DateTimeOffset? DateStart { get; set; }
        //public DateTimeOffset? DateEnd { get; set; }

        public ReportPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
