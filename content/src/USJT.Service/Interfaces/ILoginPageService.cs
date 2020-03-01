using USJT.Models.Structs;
using System.Threading.Tasks;

namespace USJT.Service.Interfaces
{
    public interface ILoginPageService
    {
        Task<bool> LoginAsync(Login login);
    }
}