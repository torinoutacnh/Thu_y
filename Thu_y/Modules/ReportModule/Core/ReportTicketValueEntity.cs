using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    public class ReportTicketValueEntity : Entity
    {
        public string AttributeId { get; set; }
        public string Value { get; set; }

        public string ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual ReportTicketEntity ReportTicket { get; set; }
    }
}
