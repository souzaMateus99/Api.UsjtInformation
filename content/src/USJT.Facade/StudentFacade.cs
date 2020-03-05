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
            var mainPage = await _loginService.LoginAsync(login);

            if(mainPage.Content.Contains(login.Matricula))
            {
                var schedulerClassPage = await _schedulerService.GetClassSchedulerAsync(mainPage);

                var scheduleTable = schedulerClassPage.Find("tbody", By.Class("tabela-horario")).FirstOrDefault();

                var tableRows = scheduleTable.SelectNodes("//tr");

                var tableLines = tableRows.Select((n, i) => n.SelectNodes($"//td[{i + 1}]"));

                var classes = tableLines
                    .Where(c => !Regex.IsMatch(c.First().InnerText.Trim(), @"\d{2}:\d{2}"))
                    .Select(c => c.Select(n => new Subject { Title = n.InnerText.Trim() }));

                var hours = tableLines
                    .FirstOrDefault(c => Regex.IsMatch(c.First().InnerText.Trim(), @"\d{2}:\d{2}"))
                    .Select(n => n.InnerText.Trim());

                return classes
                    .SelectMany(c => c.Select((s, i) => 
                        {
                            var startHour = hours.ElementAt(i).Split('-').FirstOrDefault().Trim();
                            var finishHour = hours.ElementAt(i).Split('-').LastOrDefault().Trim();
                            var clas = Regex.Match(s.Title, @"\([\w\d-]+\)").Value;

                            s.ClassHour = new ClassHour{ StartHour = startHour, FinishHour = finishHour };
                            
                            s.Classroom = s.Title.Split(clas).LastOrDefault().Trim();
                            s.Title = s.Title.Split(clas).FirstOrDefault().Trim();

                            return s;
                        })
                    );
            }

            return Enumerable.Empty<Subject>();
        }
    }
}