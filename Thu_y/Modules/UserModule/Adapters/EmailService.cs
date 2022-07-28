using System.Net;
using System.Net.Mail;
using Thu_y.Infrastructure.Utils.Constant;
using Thu_y.Infrastructure.Utils.Exceptions;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;


        private string EMAIL_SENDER = MailSettingModel.Instance.FromAddress;
        private string EMAIL_SENDER_PASSWORD = MailSettingModel.Instance.Smtp.Password;
        private string EMAIL_SENDER_HOST = MailSettingModel.Instance.Smtp.Host;
        private int EMAIL_SENDER_PORT = Convert.ToInt16(MailSettingModel.Instance.Smtp.Port);
        private bool EMAIL_IsSSL = Convert.ToBoolean(MailSettingModel.Instance.Smtp.EnableSsl);
        public EmailService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void SendMail(SendMailModel model)
        {
            switch (model.Type)
            {
                case MailType.Verify:
                    CreateVerifyMail(model);
                    break;
                case MailType.ResetPassword:
                    CreateResetPassMail(model);
                    break;
                default:
                    break;
            }
        }

        private void CreateVerifyMail(SendMailModel model)
        {
            try
            {
                var mailmsg = new MailMessage
                {
                    IsBodyHtml = false,
                    From = new MailAddress(MailSettingModel.Instance.FromAddress, MailSettingModel.Instance.FromDisplayName),
                    Subject = EmailSubjectConstant.Verify
                };
                mailmsg.To.Add(model.Email);

                mailmsg.Body = $"[Account: {model.Account}] Verify Account as link: " +
                               $"https://chicucthuy.amazingtech.vn/verify?token={model.Token}&username={model.Account}";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = EMAIL_SENDER_HOST;

                smtp.Port = EMAIL_SENDER_PORT;

                smtp.EnableSsl = EMAIL_IsSSL;

                var network = new NetworkCredential(EMAIL_SENDER, EMAIL_SENDER_PASSWORD);
                smtp.Credentials = network;

                smtp.Send(mailmsg);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
           
        }

        private void CreateResetPassMail(SendMailModel model)
        {
            try
            {
                var mailmsg = new MailMessage
                {
                    IsBodyHtml = false,
                    From = new MailAddress(MailSettingModel.Instance.FromAddress, MailSettingModel.Instance.FromDisplayName),
                    Subject = EmailSubjectConstant.ResetPass
                };
                mailmsg.To.Add(model.Email);

                mailmsg.Body = $"[Account: {model.Account}] Reset password as link: " +
                               $"https://chicucthuy.amazingtech.vn/forgotpass?={model.Token}&username={model.Account}";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = EMAIL_SENDER_HOST;

                smtp.Port = EMAIL_SENDER_PORT;

                smtp.EnableSsl = EMAIL_IsSSL;
                var network = new NetworkCredential(EMAIL_SENDER, EMAIL_SENDER_PASSWORD);
                smtp.Credentials = network;
                smtp.Send(mailmsg);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
            
        }
    }
}
