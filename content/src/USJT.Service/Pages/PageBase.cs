using System;
using System.Linq;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Threading.Tasks;

namespace USJT.Service.Pages
{
    public abstract class PageBase
    {
        private readonly ScrapingBrowser _browser;
        
        public PageBase(ScrapingBrowser browser)
        {
            _browser = browser;
        }

        public bool Login()
        {
            return true;
        }
        
        public async Task<bool> LogoutAsync()
        {
            var uri = new Uri(@"https://aluno.usjt.br/SOL/aluno/index.php/index/seguranca/logout");
            var page = await _browser.NavigateToPageAsync(uri, HttpVerb.Post, "__ajax:1", "application/x-www-form-urlencoded; charset=UTF-8");

            var loginForm = page.Find("form", By.Class("dev-form dev-form-login"));

            return loginForm.Any();
        }

        public string ExpandSideMenu()
        {
            return string.Empty;
        }
    }
}