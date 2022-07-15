using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.UserModule.Core
{
    public class UserEntity : Entity
    {
        public string? Name { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public SexType Sex { get; set; }
        public RoleType Role { get; set; }

        public virtual ICollection<UserScheduleEntity>? UserSchedules { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
