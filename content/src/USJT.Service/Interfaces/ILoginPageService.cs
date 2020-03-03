using USJT.Models.Structs;
using ScrapySharp.Network;
using System.Threading.Tasks;

namespace USJT.Service.Interfaces
{
    public interface ILoginPageService
    {
        Task<WebPage> LoginAsync(Login login);
    }
}