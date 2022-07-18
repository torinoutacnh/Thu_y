using Thu_y.Modules.UserModule.Model;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IEmailService
    {
        void SendMail(SendMailModel model);
    }
}
