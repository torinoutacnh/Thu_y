using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.UserModule.Model
{
    public class UserGetListModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public SexType Sex { get; set; }
        public RoleType Role { get; set; }
    }
}
