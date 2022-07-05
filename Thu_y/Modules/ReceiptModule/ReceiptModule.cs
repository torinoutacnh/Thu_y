using Thu_y.Modules.ReceiptModule.Adapters;
using Thu_y.Modules.ReceiptModule.Endpoints;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Utils.Module;

namespace Thu_y.Modules.ReceiptModule
{
    public class ReceiptModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapReceiptEndpoints();
            endpoints.MapAllocateReceiptEndpoints();

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IReceiptAllocateRepository, ReceiptAllocateRepository>();
            services.AddScoped<IReceiptReportRepository, ReceiptReportRepository>();
            services.AddScoped<IReceiptService, ReceiptService>();

            return services;
        }
    }
}
