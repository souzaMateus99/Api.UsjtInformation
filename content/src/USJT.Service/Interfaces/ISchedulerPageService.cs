using ScrapySharp.Network;
using System.Threading.Tasks;

namespace USJT.Service.Interfaces
{
    public interface ISchedulerPageService
    {
        Task<WebPage> GetClassSchedulerAsync(WebPage page);
    }
}