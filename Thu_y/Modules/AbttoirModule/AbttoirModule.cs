using Thu_y.Modules.AbttoirModule.Adapters;
using Thu_y.Modules.AbttoirModule.Endpoints;
using Thu_y.Modules.AbttoirModule.Ports;
using Thu_y.Utils.Module;

namespace Thu_y.Modules.AbttoirModule
{
    public class AbttoirModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapAbttoirEndpoints();
            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            //services.AddScoped<IAbattoirDetailRepository, AbattoirDetailRepository>();
            //services.AddScoped<IAbattoirRepository, AbattoirRepository>();
            //services.AddScoped<IAbattoirService, AbattoirService>();
            return services;
        }
    }
}
