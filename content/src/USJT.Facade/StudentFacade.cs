using ScrapySharp.Html;
using USJT.Service.Enums;
using USJT.Models.Structs;
using USJT.Facade.Interfaces;
using System.Threading.Tasks;
using USJT.Service.Interfaces;
using USJT.Models.Extensions;

namespace USJT.Facade
{
    public class StudentFacade : IStudentFacade
    {
        private readonly ILoginPageService _loginService;
        private readonly ISchedulerPageService _schedulerService;
        
        public StudentFacade(ILoginPageService loginService, ISchedulerPageService schedulerService)
        {
            _loginService = loginService;
            _schedulerService = schedulerService;
        }
        
        public async Task<string> GetClassScheduleAsync(Login login)
        {
            var mainPage = await _loginService.LoginAsync(login);

            if(mainPage.Content.Contains(login.Matricula))
            {
                var schedulerClassPage = await _schedulerService.GetClassSchedulerAsync(mainPage);

                return schedulerClassPage.Content;
            }

            return string.Empty;
        }
    }
}