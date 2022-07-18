using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.AbttoirModule.Model
{
    public class AbttoirPagingModel
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int PageNumber { get; set; }
        [Range(0, int.MaxValue)]
        [Required]
        public int PageSize { get; set; }
        public string? Id { get; set; }

        public AbttoirPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
