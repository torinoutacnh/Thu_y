using System.ComponentModel.DataAnnotations;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ShareModule.Model
{
    public class AnimalPagingModel
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int PageNumber { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int PageSize { get; set; }

        [Required]
        public string Id { get; set; }

        public AnimalPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
