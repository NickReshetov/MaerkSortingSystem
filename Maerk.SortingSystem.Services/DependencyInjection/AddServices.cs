using Maerk.SortingSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Maerk.SortingSystem.Services.DependencyInjection
{

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISortingJobService, SortingJobService>();

            return services;
        }
    }
}
