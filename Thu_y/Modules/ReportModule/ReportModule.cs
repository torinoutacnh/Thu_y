using Thu_y.Modules.ReportModule.Adapters;
using Thu_y.Modules.ReportModule.Endpoints;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Module;

namespace Thu_y.Modules.ReportModule
{
    public class ReportModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapFormEndpoints();
            endpoints.MapReportEndpoints();
            endpoints.MapSealConfigEndpoints();
            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormAttributeRepository, FormAttributeRepository>();
            services.AddScoped<IReportTicketRepository, ReportTicketRepository>();
            services.AddScoped<IReportTicketValueRepository, ReportTicketValueRepository>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISealTabRepository, SealTabRepository>();
            services.AddScoped<IListAnimalRepository, ListAnimalRepository>();
            services.AddScoped<ISealConfigRepository, SealConfigRepository>();
            services.AddScoped<ISealConfigService, SealConfigService>();

            return services;
        }
    }
}
