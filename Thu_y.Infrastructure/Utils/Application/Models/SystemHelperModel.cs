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
}
