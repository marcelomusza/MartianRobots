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

            // DEPENDENCY INJECTION
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IMartianEngine, MartianEngine>();

            //Stablishing the relative location for the local db, available to be ran on any environment
            string conn = Configuration.GetConnectionString("MartianRobotsDb");
            if (conn.Contains("%CONTENTROOTPATH%"))
                conn = conn.Replace("%CONTENTROOTPATH%", contentRootPath);     

            //Setting Database Connection
            if(hostEnvironment.EnvironmentName.Equals("Development"))
            {
                services.AddDbContext<MartianRobotsDBContext>(cfg =>                
                    cfg.UseSqlServer(conn)
                );
            }
            else
            {
                //Reserved for Azure
            }
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
        }
    }
}
