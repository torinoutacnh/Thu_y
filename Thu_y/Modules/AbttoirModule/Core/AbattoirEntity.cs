using Thu_y.Infrastructure.Model;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.AbttoirModule.Core
{
    public class AbattoirEntity : Entity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
