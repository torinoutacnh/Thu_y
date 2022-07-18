using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.AbttoirModule.Core
{
    [Table("Abattoir")]
    public class AbattoirEntity : Entity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
