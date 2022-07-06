using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    public class FormAttributeEntity : Entity
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string ControlType { get; set; }
        public int SortNo { get; set; }

        public string FormId { get; set; }
        [ForeignKey("FormId")]
        public virtual FormEntity Form { get; set; }

        public virtual ICollection<ReportTicketValueEntity> ReportTicketValues { get; set; }
    }
}
