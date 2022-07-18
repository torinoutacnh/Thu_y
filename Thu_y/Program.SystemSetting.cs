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

        public static IServiceCollection AddJwtSetting(this IServiceCollection services, JWTSettingModel jwtSettingModel)
        {
            JWTSettingModel.Instance = jwtSettingModel ?? new JWTSettingModel();

            return services;
        }

        public static IApplicationBuilder UseSystemSetting(this IApplicationBuilder app)
        {
            SystemHelperModel.Configs = app.ApplicationServices.GetRequiredService<IConfiguration>();

            return app;
        }

        public static IServiceCollection AddMailSetting(this IServiceCollection services, MailSettingModel mailSetting)
        {
            MailSettingModel.Instance = mailSetting ?? new MailSettingModel();

            return services;
        }
    }
}
