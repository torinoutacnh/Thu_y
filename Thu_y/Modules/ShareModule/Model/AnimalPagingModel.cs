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
        public string? Id { get; set; }

        public string? Name { get; set; }
        public int? DayAge { get; set; }
        public SexType? Sex { get; set; }

        public AnimalPagingModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
