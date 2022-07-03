using Thu_y.Modules.Module;
using Thu_y.Modules.UserModule.Adapters;
using Thu_y.Modules.UserModule.Endpoints;
using Thu_y.Modules.UserModule.Ports;

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
            return services;
        }
    }
}
