using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int Page { get; set; }
        [Required]
        public string CodeName { get; set; }
        [Required]
        public string CodeNumber { get; set; }
        [Required]
        public DateTimeOffset EffectiveDate { get; set; }

        public ICollection<ReceiptAllocateModel> Allocates { get; set; }
    }
}
