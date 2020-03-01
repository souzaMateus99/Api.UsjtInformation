using System;
using USJT.Service.Pages;
using ScrapySharp.Network;
using USJT.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace USJT.Facade.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebCrawlerServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoginPageService, LoginPageService>();
            
            services.AddSingleton(p => 
            {
                var browser = new ScrapingBrowser();

                browser.AllowMetaRedirect = true;
                browser.KeepAlive = true;
                browser.Timeout = TimeSpan.FromMinutes(2);

                return browser;
            });
        }
    }
}