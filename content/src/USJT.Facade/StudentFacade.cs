using USJT.Models.Structs;
using USJT.Facade.Interfaces;
using System.Threading.Tasks;
using USJT.Service.Interfaces;

namespace USJT.Facade
{
    public class StudentFacade : IStudentFacade
    {
        private readonly ILoginPageService _loginService;
        
        public StudentFacade(ILoginPageService loginService)
        {
            _loginService = loginService;
        }
        
        public async Task<string> GetClassScheduleAsync(Login login)
        {
            var logged = await _loginService.LoginAsync(login);

            if(logged)
            {

            }

            return string.Empty;
        }
    }
}