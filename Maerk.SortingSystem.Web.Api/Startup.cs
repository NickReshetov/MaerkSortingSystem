using System;
using AutoMapper;
using Maerk.SortingSystem.DataAccess.DependencyInjection;
using Maerk.SortingSystem.Services.DependencyInjection;
using Maerk.SortingSystem.Web.Api.Extensions;
using Maerk.SortingSystem.Worker.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Maerk.SortingSystem.Web.Api
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddLogging();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddSingleton(_logger);

            services.AddAutoMapper(assemblies);

            services.AddServices();

            services.AddWorkerServices();

            services.AddDataAccess();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Maersk Sorting System Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.ConfigureExceptionHandler(_logger);
            }
            else
            {
                app.ConfigureExceptionHandler(_logger);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maersk Sorting System Api");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
