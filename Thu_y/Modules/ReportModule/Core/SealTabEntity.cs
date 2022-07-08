using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    public class SealTabEntity : Entity
    {
        public string? CodeSeal { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }

        public string? ReportTicketId { get; set; }

        [ForeignKey("ReportTicketId")]
        public virtual ReportTicketEntity? ReportTicket { get; set; }
    }
}
