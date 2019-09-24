using System.Threading;
using Maerk.SortingSystem.Worker.Handlers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Maerk.SortingSystem.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            var backgroundWorkerThread = new Thread(WorkerHandlers.ProcessSortingJobHandlerAsync)
            {
                IsBackground = true
            };

            backgroundWorkerThread.Start(webHost.Services);

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration));
    }
}
