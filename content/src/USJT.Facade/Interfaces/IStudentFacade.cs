using USJT.Models.Model;
using USJT.Models.Structs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace USJT.Facade.Interfaces
{
    public interface IStudentFacade
    {
        Task<IEnumerable<Subject>> GetClassScheduleAsync(Login login);
    }
}