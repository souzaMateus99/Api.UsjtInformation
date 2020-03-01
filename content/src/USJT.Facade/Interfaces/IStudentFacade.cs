using USJT.Models.Structs;
using System.Threading.Tasks;

namespace USJT.Facade.Interfaces
{
    public interface IStudentFacade
    {
        Task<string> GetClassScheduleAsync(Login login);
    }
}