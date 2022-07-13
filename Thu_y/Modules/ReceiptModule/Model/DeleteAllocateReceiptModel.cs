using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class DeleteAllocateReceiptModel
    {
        [Required]
        public string Id { get; set; }
    }
}
