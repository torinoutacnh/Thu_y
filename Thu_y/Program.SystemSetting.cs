using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y
{
    public static class ProgramSystemSetting
    {
        public static IServiceCollection AddSystemSetting(this IServiceCollection services, SystemHelperModel systemSettingModel)
        {
            SystemHelperModel.Instance = systemSettingModel ?? new SystemHelperModel();

            return services;
        }

        public static IApplicationBuilder UseSystemSetting(this IApplicationBuilder app)
        {
            SystemHelperModel.Configs = app.ApplicationServices.GetRequiredService<IConfiguration>();

            return app;
        }

        public static IServiceCollection AddJWTSetting(this IServiceCollection services, JWTSettingModel JwtSettingModel)
        {
            JWTSettingModel.Instance = JwtSettingModel ?? new JWTSettingModel();

            return services;
        }
    }
}
