using System;
using System.Linq;
using USJT.Facade;
using USJT.Service.Pages;
using ScrapySharp.Network;
using USJT.Facade.Interfaces;
using USJT.Service.Constants;
using System.Threading.Tasks;
using USJT.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace USJT.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            services.AddSingleton<ILoginPageService, LoginPageService>();
            services.AddSingleton<IStudentFacade, StudentFacade>();
            services.AddSingleton(p => 
            {
                var browser = new ScrapingBrowser();

                browser.AllowMetaRedirect = true;
                browser.KeepAlive = true;
                browser.Timeout = TimeSpan.FromMinutes(2);

                return browser;
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
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
