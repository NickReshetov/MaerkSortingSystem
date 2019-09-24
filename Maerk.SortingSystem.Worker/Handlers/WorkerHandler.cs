using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Maerk.SortingSystem.Worker.Handlers
{
    public static class WorkerHandlers
    {
        private const int SleepingIntervalInSeconds = 5;

        public static async void ProcessSortingJobHandlerAsync(object obj)
        {
            var provider = obj as IServiceProvider;

            if (provider == null)
                throw new ArgumentException($"Passed in thread parameter was not of type {nameof(IServiceProvider)}",nameof(obj));

            while (true)
            {
                using (var scope = provider.CreateScope())
                {
                    var workerService = scope.ServiceProvider.GetRequiredService<IWorker>();

                    var messageQueue = scope.ServiceProvider.GetRequiredService<ConcurrentQueue<SortingJobDto>>();

                    messageQueue.TryDequeue(out var sortingJobDto);

                    if (sortingJobDto != null)
                        await workerService.ProcessSortingJobAsync(sortingJobDto);
                }

                var sleepingInterval = TimeSpan.FromSeconds(SleepingIntervalInSeconds);

                Thread.Sleep(sleepingInterval);
            }
        }
    }
}
