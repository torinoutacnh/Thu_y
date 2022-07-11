using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptReportModel
    {
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string ReceiptAllocateId { get; set; }
        [Required]
        public string ReceiptName { get; set; }
        public string CodeName { get; set; }
        public string CodeNumber { get; set; }
        [Required]
        public DateTimeOffset? DateUse { get; set; }
        [Required]
        public int? PageUse { get; set; }
    }
}
