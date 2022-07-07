using Thu_y.Modules.ShareModule.Adapters;
using Thu_y.Modules.ShareModule.Endpoints;
using Thu_y.Modules.ShareModule.Ports;
using Thu_y.Utils.Module;

namespace Thu_y.Modules.ShareModule
{
    public class ShareModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapAnimalEndpoints();
            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IVacineRepository, VacineRepository>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IVacineService, VacineService>();
            return services;
        }
    }
}
