using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    [Table("SealTab")]
    public class SealTabEntity : Entity
    {
        public SealTabEntity()
        {
            Price = 1M;
        }

        public string? CodeSeal { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Id_Pricing { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string ReportTicketId { get; set; }

        [ForeignKey("ReportTicketId")]
        public virtual ReportTicketEntity? ReportTicket { get; set; }
    }
}
