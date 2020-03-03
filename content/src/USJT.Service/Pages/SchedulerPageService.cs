using System;
using ScrapySharp.Network;
using System.Threading.Tasks;
using USJT.Service.Interfaces;

namespace USJT.Service.Pages
{
    public class SchedulerPageService : PageBase, ISchedulerPageService
    {
        private const string SIDE_BAR_MENU_TITLE = "confirmados";
        
        public SchedulerPageService(ScrapingBrowser browser)
            : base(browser)
        { }
        
        public async Task<WebPage> GetClassSchedulerAsync(WebPage page)
        {
            var scheduleItemMenu = GetSideBarMenuItem(SIDE_BAR_MENU_TITLE, page);

            return await browser.NavigateToPageAsync(new Uri(scheduleItemMenu.Value));
        }
    }
}