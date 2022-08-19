using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Utils.Infrastructure.Application
{
    public static class SystemHelper
    {
        public static SystemHelperModel Setting => SystemHelperModel.Instance;
        public static IConfiguration Configs => SystemHelperModel.Configs;
        public static string AppDb => SystemHelperModel.Configs.GetConnectionString("DefaultConnection");

        public static DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow;
    }
}
