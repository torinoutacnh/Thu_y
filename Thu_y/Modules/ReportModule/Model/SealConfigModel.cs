using System.ComponentModel.DataAnnotations;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class SealConfigModel
    {
        public string Id { get; set; }

        [Required]
        public string SealName { get; set; }

        [Required]
        public string SealCode { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
