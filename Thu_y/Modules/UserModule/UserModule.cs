﻿using Thu_y.Modules.UserModule.Adapters;
using Thu_y.Modules.UserModule.Endpoints;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Module;

namespace Thu_y.Modules.UserModule
{
    public class UserModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapScheduleEndpoints();
            endpoints.MapUserEndpoints();
            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IUserScheduleRepository, UserScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
