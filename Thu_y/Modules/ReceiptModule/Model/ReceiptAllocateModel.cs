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
        [Range(0,int.MaxValue)]
        [Required]
        public int TotalPage { get; set; }

        public string UserName { get; set; }
        public string CodeName { get; set; }
        public string CodeNumber { get; set; }
        public string ReceiptName { get; set; }
    }
}
