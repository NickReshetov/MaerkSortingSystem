using Maerk.SortingSystem.DataAccess.Repositories;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Maerk.SortingSystem.DataAccess.DependencyInjection
{

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddTransient<ISortingJobRepository, SortingJobRepository>();

            return services;
        }
    }
}
