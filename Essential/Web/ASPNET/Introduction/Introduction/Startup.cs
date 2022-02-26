using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Introduction.Data;
using Introduction.Data.Attributes;
using Introduction.Data.LoggerProviders;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Introduction
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
            services.AddControllersWithViews();
            services.AddDbContext<NorthwindContext>(options =>
                options
                    //.UseLazyLoadingProxies() //install-package microsoft.entityframeworkcore.proxies
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<ViewOptions>(Configuration.GetSection(ViewOptions.View));

            services.AddSingleton<IValidationAttributeAdapterProvider,
                CustomValidationAttributeAdapterProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            ILogger logger = loggerFactory.CreateLogger("FileLogger");

            logger.LogInformation($"Start application: application location => {AppContext.BaseDirectory}, folder path => {Path.GetFullPath(Directory.GetCurrentDirectory())}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
