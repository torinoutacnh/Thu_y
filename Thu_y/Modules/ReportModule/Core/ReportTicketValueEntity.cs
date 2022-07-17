using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    public class ReportTicketValueEntity : Entity
    {
        public string? Value { get; set; }
        public string? AttributeName { get; set; }
        public string? AttributeDataType { get; set; }
        public string? AttributeControlType { get; set; }
        public string? FormNumber { get; set; }
        public string? FormName { get; set; }
        public string? FormCode { get; set; }
        public string? AttributeId { get; set; }
        public int? Sort { get; set; }
        public string ReportId { get; set; }

        [ForeignKey("ReportId")]
        public virtual ReportTicketEntity? ReportTicket { get; set; }
        [ForeignKey("AttributeId")]
        public virtual FormAttributeEntity? Attribute { get; set; }
    }
}
