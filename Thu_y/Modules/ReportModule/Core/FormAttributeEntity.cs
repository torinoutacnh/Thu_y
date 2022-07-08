using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Core
{
    public class FormAttributeEntity : Entity
    {
        public string? Name { get; set; }
        public DataTypes DataType { get; set; }
        public ControlTypes ControlType { get; set; }
        public int? SortNo { get; set; }
        public string? Col_Design { get; set; }
        public string? api_DropDownlist { get; set; }

        public string? FormId { get; set; }
        [ForeignKey("FormId")]
        public virtual FormEntity? Form { get; set; }

        public virtual ICollection<ReportTicketValueEntity>? ReportTicketValues { get; set; }
    }
}
