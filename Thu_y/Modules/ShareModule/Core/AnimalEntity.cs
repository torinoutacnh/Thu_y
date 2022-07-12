using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ShareModule.Core
{
    public class AnimalEntity : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DayAge { get; set; }
        public SexType Sex { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Pricing { get; set; }
        public virtual ICollection<VacineEntity>? Vacines { get; set; }
    }
}
