using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Maerk.SortingSystem.Worker.Handlers
{
    class SortingJobHandler
    {
        private static void SortingJobsHandlerAsync(object obj)
        {
            //// Here we check that the provided parameter is, in fact, an IServiceProvider
            //IServiceProvider provider = obj as IServiceProvider
            //                            ?? throw new ArgumentException($"Passed in thread parameter was not of type {nameof(IServiceProvider)}", nameof(obj));

            //// Using an infinite loop for this demonstration but it all depends on the work you want to do.
            //while (true)
            //{
            //    //    // Here we create a new scope for the IServiceProvider so that we can get already built objects from the Inversion Of Control Container.
            //    using (IServiceScope scope = provider.CreateScope())
            //    {
            //        // Here we retrieve the singleton instance of the BackgroundWorker.
            //        var workerService = scope.ServiceProvider.GetRequiredService<IWorkerService>();

            //        //   And we execute it, which will log out a number to the console
            //        workerService.ProcessSortingJob();
            //    }

            //    // This is only placed here so that the console doesn't get spammed with too many log lines
            //    Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
