using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.UserModule.Model
{
    public class ResponseLoginModel
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public RoleType Role { get; set; }
        public string Token { get; set; }
    }
}
