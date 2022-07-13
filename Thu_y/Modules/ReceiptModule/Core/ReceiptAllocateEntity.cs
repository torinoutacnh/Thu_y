using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReceiptModule.Core
{
    public class ReceiptAllocateEntity : Entity
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? CodeName { get; set; }
        public string? CodeNumber { get; set; }
        public int? Amount { get; set; }
        public int? TotalPage { get; set; }

        public string? ReceiptName { get; set; }
        public string ReceiptId { get; set; }
        [ForeignKey("ReceiptId")]
        public virtual ReceiptEntity? Receipt { get; set; }
    }
}
