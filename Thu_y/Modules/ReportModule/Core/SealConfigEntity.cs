using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Core
{
    public class SealConfigEntity : Entity
    {
        public string? SealName { get; set; }
        public string? SealCode { get; set; }

        [Column(TypeName =("decimal(18,4)"))]
        public decimal UnitPrice { get; set; }
        public SealType Type { get; set; }
    }
}
