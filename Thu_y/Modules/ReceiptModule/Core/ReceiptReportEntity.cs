using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReceiptModule.Core
{
    [Table("ReceiptReport")]
    public class ReceiptReportEntity : Entity
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }

        public string? ReceiptAllocateId { get; set; }

        public string? ReceiptName { get; set; }
        public string? CodeName { get; set; }
        public string? CodeNumber { get; set; }

        public DateTimeOffset? DateUse { get; set; }
        public int? PageUse { get; set; }
    }
}
