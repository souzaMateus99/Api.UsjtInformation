using System;
using System.Linq;
using ScrapySharp.Html;
using USJT.Service.Enums;
using USJT.Models.Structs;
using ScrapySharp.Network;
using USJT.Service.Constants;
using System.Threading.Tasks;
using USJT.Models.Extensions;
using USJT.Service.Interfaces;
using System.Collections.Generic;

namespace USJT.Service.Pages
{
    public class LoginPageService : PageBase, ILoginPageService
    {        
        private const string MATRICULA_KEY = "matricula";
        private const string SENHA_KEY = "senha";
        
        public LoginPageService(ScrapingBrowser browser)
            : base(browser)
        {
            browser.NavigateToPage(new Uri(PageContext.LOGIN_PAGE_CONTEXT));
        }

        public async Task<WebPage> LoginAsync(Login login)
        {
            var parameters = RequestsParameters.LoginParameters;
            parameters[MATRICULA_KEY] = login.Matricula;
            parameters[SENHA_KEY] = login.Senha;
            
            var requestParameters = GetParameterString(parameters);

            await browser.NavigateToPageAsync(new Uri(PageContext.LOGIN_URL_CONTEXT), HttpVerb.Post, requestParameters, ContentTypes.FORM_URL_ENCONDED);
            return await browser.NavigateToPageAsync(new Uri(PageContext.MAIN_PAGE_CONTEXT));
        }
    }
}