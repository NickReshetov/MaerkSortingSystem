using System.Collections.Concurrent;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Maerk.SortingSystem.Services.DependencyInjection
{

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISortingJobService, SortingJobService>();

            services.AddSingleton<ConcurrentQueue<SortingJobDto>>();

            return services;
        }
    }
}
