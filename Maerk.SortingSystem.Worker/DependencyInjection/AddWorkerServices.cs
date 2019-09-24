using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Maerk.SortingSystem.Worker.DependencyInjection
{

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services)
        {
            services.AddSingleton<IWorker, Worker>();

            return services;
        }
    }
}
