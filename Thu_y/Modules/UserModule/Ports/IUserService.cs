using Thu_y.Modules.UserModule.Model;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IUserService
    {
        bool CreateUser(UserModel model);
        bool UpdateUser(UserModel model);
        bool DeleteUser(string id);
    }
}
