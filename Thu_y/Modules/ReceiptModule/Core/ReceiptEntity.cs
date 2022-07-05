using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReceiptModule.Core
{
    public class ReceiptEntity : Entity
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public string CodeName { get; set; }
        public string CodeNumber { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }

        public virtual ICollection<ReceiptAllocateEntity> Allocates { get; set; }
    }
}
