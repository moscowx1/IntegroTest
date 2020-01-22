using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Logging;
using System.IO;
using ServerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(op => op.JsonSerializerOptions.IgnoreNullValues = true);

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddLogging(builder => builder
                .AddConsole());

            services.AddCors();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors( builder => 
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../ClientApp";
                spa.UseAngularCliServer("start");
            });

            SeedExcelFiles.Seed(services.GetRequiredService<DataContext>());
        }
    }
}
