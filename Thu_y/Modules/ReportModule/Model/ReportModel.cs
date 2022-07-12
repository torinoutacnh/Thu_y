using System.ComponentModel.DataAnnotations;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public string FormId { get; set; }
        public ReportType Type { get; set; }
        public ICollection<ReportValueModel> Values { get; set; }
        public ICollection<ListAnimalModel>? ListAnimals { get; set; }
        public  ICollection<SealTabModel>? SealTabs { get; set; }
    }
}
