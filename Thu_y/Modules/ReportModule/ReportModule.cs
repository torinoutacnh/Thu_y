using Thu_y.Modules.Module;
using Thu_y.Modules.ReportModule.Adapters;
using Thu_y.Modules.ReportModule.Endpoints;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule
{
    public class ReportModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapFormEndpoints();
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
            return services;
        }
    }
}
