using System.Linq;
using ScrapySharp.Html;
using USJT.Models.Model;
using USJT.Models.Structs;
using System.Threading.Tasks;
using USJT.Facade.Interfaces;
using USJT.Service.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using USJT.Models.Enums;

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
        
        public async Task<IEnumerable<Subject>> GetClassScheduleAsync(Login login)
        {
            var classesSchedule = Enumerable.Empty<Subject>();
            
            var mainPage = await _loginService.LoginAsync(login);

            if(mainPage.Content.Contains(login.Matricula))
            {
                var schedulerClassPage = await _schedulerService.GetClassSchedulerAsync(mainPage);

                var scheduleTable = schedulerClassPage.Find("tbody", By.Class("tabela-horario")).FirstOrDefault();

                var tableRows = scheduleTable.SelectNodes("//tr");

                var classes = tableRows.Select((n, i) => n.SelectNodes($"//td[{i + 1}]"));

                var test = classes
                    .Where(c => !Regex.IsMatch(c.First().InnerText.Trim(), @"\d{2}:\d{2}"))
                    .Select(c => c.Select(n => new Subject { Title = n.InnerText.Trim() }));

                var test2 = classes
                    .Where(c => Regex.IsMatch(c.First().InnerText.Trim(), @"\d{2}:\d{2}"))
                    .Select(c => c.Select(n => new ClassHour { StartHour = n.InnerText.Split('-')[0].Trim(), FinishHour = n.InnerText.Split('-')[1].Trim() }));
            }

            return classesSchedule;
        }
    }
}