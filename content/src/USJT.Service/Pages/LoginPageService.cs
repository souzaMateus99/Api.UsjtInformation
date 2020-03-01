using System;
using System.Web;
using System.Linq;
using USJT.Models.Structs;
using ScrapySharp.Html;
using ScrapySharp.Network;
using USJT.Service.Constants;
using System.Threading.Tasks;
using USJT.Service.Interfaces;
using System.Collections.Generic;

namespace USJT.Service.Pages
{
    public class LoginPageService : PageBase, ILoginPageService
    {
        public LoginPageService(ScrapingBrowser browser)
            : base(browser)
        {
            
        }

        public async Task<bool> LoginAsync(Login login)
        {
            var uri = new Uri(PageContext.LOGIN_URL_CONTEXT);

            var parameters = RequestsParameters.LoginParameters;
            parameters["matricula"] = login.Matricula;
            parameters["senha"] = login.Senha;
            
            var requestParameters = GetParameterString(parameters);

            await browser.NavigateToPageAsync(uri, HttpVerb.Post, requestParameters, ContentTypes.FORM_URL_ENCONDED);
            page = await browser.NavigateToPageAsync(new Uri(PageContext.MAIN_PAGE_CONTEXT));
            
            return page.Find("div", By.Class("left")).Any();
        }
    }
}