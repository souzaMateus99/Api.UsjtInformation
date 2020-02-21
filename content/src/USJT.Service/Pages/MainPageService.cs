using System;
using ScrapySharp.Network;
using System.Threading.Tasks;
using USJT.Service.Interfaces;

namespace USJT.Service.Pages
{
    public class MainPageService : PageBase, IMainPageService
    {
        public MainPageService()
            : base(new ScrapingBrowser())
        {
            
        }
    }
}