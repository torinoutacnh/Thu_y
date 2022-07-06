using System.ComponentModel.DataAnnotations;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string ApproveId { get; set; }
        public string ApproveName { get; set; }
        [Required]
        public string AttributeId { get; set; }
        [Required]
        public string SerialNumber { get; set; }

        public ReportType Type { get; set; }
        [Required]
        public ICollection<ReportValueModel> Values { get; set; }
    }
}
