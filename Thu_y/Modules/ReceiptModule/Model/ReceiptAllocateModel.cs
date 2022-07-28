using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptAllocateModel
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ReceiptId { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int Amount { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string CodeName { get; set; }

        [Required]
        public string CodeNumber { get; set; }

        [Required]
        public string ReceiptName { get; set; }
        public int TotalPage { get; set; }
        public int RemainPage { get; set; }
    }
}
