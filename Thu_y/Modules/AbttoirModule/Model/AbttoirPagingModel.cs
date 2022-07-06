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
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public AbttoirPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
