using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.UserModule.Model
{
    public class SendMailModel
    {
        public string Account { get; set; }
        public string Token { get; set; }
        public MailType Type { get; set; }
        public string Email { get; set; }

    }
}
