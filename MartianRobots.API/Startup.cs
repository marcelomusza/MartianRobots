using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MartianRobots.Infrastructure.Data;
using MartianRobots.Application.Utilities;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Engines;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure;

namespace MartianRobots
{
    public class Startup
    {

        private string contentRootPath = "";
        private readonly IHostEnvironment hostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            contentRootPath = env.ContentRootPath;
            hostEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Console.WriteLine(contentRootPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfile));

            // DEPENDENCY INJECTION
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IMartianEngine, MartianEngine>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Stablishing the relative location for the local db, available to be ran on any environment
            string conn = Configuration.GetConnectionString("MartianRobotsDb");
            if (conn.Contains("%CONTENTROOTPATH%"))
                conn = conn.Replace("%CONTENTROOTPATH%", contentRootPath);   
            
            //The Connectiong String will vary depending if the App is being ran Locally, or on Azure
            services.AddDbContext<MartianRobotsDBContext>(cfg =>                
                cfg.UseSqlServer(conn)
            );

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "MartianRobots",
                    Version = "v1"
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

        }
    }
}
