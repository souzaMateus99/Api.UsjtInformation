using System;
using System.Web;
using System.Linq;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Threading.Tasks;
using USJT.Service.Constants;
using System.Collections.Generic;

namespace USJT.Service.Pages
{
    public abstract class PageBase
    {
        internal readonly ScrapingBrowser browser;
        internal WebPage page;
        
        public PageBase(ScrapingBrowser browser)
        {
            this.browser = browser;
        }
        
        public async Task<bool> LogoutAsync()
        {
            var uri = new Uri(PageContext.LOGOUT_URL_CONTEXT);
            
            var page = await browser.NavigateToPageAsync(uri, HttpVerb.Post, "__ajax:1", ContentTypes.FORM_URL_ENCONDED);

            var loginForm = page.Find("form", By.Class("dev-form dev-form-login"));

            return loginForm.Any();
        }

        public string ExpandSideBarMenu()
        {
            var sideBarMenu = page.Find("div", By.Class("nav-side-menu")).First();

            if(sideBarMenu is null)
            {
                return string.Empty;
            }

            return sideBarMenu.InnerHtml;
        }

        public string GetParameterString(Dictionary<string, string> parameters)
        {
            var parametersEncoded = parameters.Select(p => 
            {
                var key = HttpUtility.UrlEncode(p.Key);
                var value = HttpUtility.UrlEncode(p.Value);

                return $"{key}={value}";
            });

            return string.Join("&", parametersEncoded);
        }
    }
}