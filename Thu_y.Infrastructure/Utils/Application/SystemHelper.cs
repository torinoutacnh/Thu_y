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
        //public static string ConnectionString => SystemHelperModel.Configs.GetConnectionString(IsProduction());

        public static DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow;

        //public static string IsProduction()
        //{
        //    var env = SystemHelperModel.Configs?.AsEnumerable().Where(c => c.Key.Equals("ASPNETCORE_ENVIRONMENT")).FirstOrDefault();
        //    if (env?.Value == null) return "Development";
        //    if (!env.Equals(new KeyValuePair<string, string>()))
        //    {
        //        return env.Value.Value.Equals("Production") ? "Production" : "Development";
        //    }
        //    return "Development";
        //}
    }
}
