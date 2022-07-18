using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Thu_y.Utils.Infrastructure.Application.Models
{
    public class SystemHelperModel
    {
        public static SystemHelperModel Instance { get; set; }
        public static IConfiguration Configs { get; set; }
        public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name;
        public string Version { get; set; }
        public string Domain { get; set; } = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        public string Host { get; set; } = System.Net.Dns.GetHostName();
    }

    public class JWTSettingModel
    {
        public static JWTSettingModel Instance { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public string RefreshSecrect { get; set; }
        public int Expires { get; set; }
        public int RefreshExpires { get; set; }
    }

    public class MailSettingModel
    {
        public static MailSettingModel Instance { get; set; }
        public static IConfiguration Configs { get; set; }
        public SmtpSetting Smtp { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
    }

    public class SmtpSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UsingCredential { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
