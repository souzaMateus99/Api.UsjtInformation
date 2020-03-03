using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using USJT.Api.Constants;
using USJT.Facade.Interfaces;
using USJT.Models.Structs;

namespace USJT.Api.Controllers
{
    [ApiController]
    [Route(ApiContext.CONTROLLER_CONTEXT)]
    public class StudentController : ControllerBase
    {
        private readonly IStudentFacade _studentFacade;
        
        public StudentController(IStudentFacade studentFacade)
        {
            _studentFacade = studentFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassScheduleAsync(string matricula, string senha)
        {
            var login = new Login { Matricula = matricula, Senha = senha };

            var content = await _studentFacade.GetClassScheduleAsync(login);

            if(string.IsNullOrEmpty(content))
            {
                return NoContent();
            }

            return Ok(content);
        }
    }
}
