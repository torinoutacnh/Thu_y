using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptPagingModel
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int PageNumber { get; set; }
        [Range(0, int.MaxValue)]
        [Required]
        public int PageSize { get; set; }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? CodeName { get; set; }
        public string? CodeNumber { get; set; }
        public bool IsEffect { get; set; }

        public ReceiptPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
