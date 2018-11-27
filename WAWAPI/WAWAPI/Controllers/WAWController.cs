using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using WAWEntity; 
using Microsoft.AspNetCore.Cors;
using WAWBusiness;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WAWAPi.Controllers
{

    [ApiController]

    [EnableCors("CorsPolicy")]
    public class WAWController : ControllerBase
    {   readonly IConfiguration _configuration;
        private WAWBusinessLogics wawserviceInstance;
        string ConnectionString = "";
        public WAWController(IConfiguration con)
        {
            
            _configuration = con;
            wawserviceInstance = new WAWBusinessLogics(_configuration["ConnectionString"]);
        }


        [HttpPost]
        [Route("api/[controller]/AddEmployee")]

        public string AddEmployee(IEmployee employee)
        {
            return wawserviceInstance.AddEmployee(employee);

        }


        [HttpGet]
        [Route("api/[controller]/getAdminUsers")]
        public List<IEmployee> getAdminUsers()
        {
            return wawserviceInstance.getAdminUsers();
        }


        [HttpPost]
        [Route("api/[controller]/AddStepCount")]
        public string AddStepCount(IEmployeeDetail employee)
        {
            return wawserviceInstance.AddStepCount(employee);
        }
        [HttpGet]
        [Route("api/[controller]/GetEmployeeStepsbyId")]
        public List<IEmployeeDetail> GetEmployeeStepsbyId(string Email, int EmpId)
        {
            var parameters = new DynamicParameters();
            return wawserviceInstance.GetEmployeeStepsbyId(Email, EmpId);
        }

        [HttpPost]
        [Route("api/[controller]/GetEmailForReminder")]
        public string GetEmailForReminder()
        {
            List<string> EmailList = new List<string>();
            string status = "";
            EmailList = wawserviceInstance.GetEmailForReminder();
            if (EmailList.Count > 0)
            {

                status= wawserviceInstance.ThreadMail(null, null, EmailList, "REMINDER", null);
            }
            return status;
        }

        [HttpGet]
        [Route("api/[controller]/getAllTeams")]
        public List<ITeam> GetAllTeams()
        {
            return wawserviceInstance.GetAllTeams();
        }


        [HttpGet]
        [Route("api/[controller]/SearchEmployeeHistoryFindAll")]
        public List<IEmployeeHistorySearchResult> SearchEmployeeHistoryFindAll(string Name,string Email,int Team,DateTime StartDate,DateTime EndDate)
        {
            return wawserviceInstance.SearchEmployeeHistoryFindAll(Name,Email,Team, StartDate,EndDate);
        }


        [HttpGet]
        [Route("api/[controller]/SearchEmployeeFindAll")]
        public List<IEmployeeSearchResult> SearchEmployeeFindAll(string Name, string Email, int Team)
        {
            return wawserviceInstance.SearchEmployeeFindAll(Name, Email, Team);
        }

        [HttpPost]
        [Route("api/[controller]/UpdateEmployeeDetail")]
        public string UpdateEmployeeDetail(IEmployee employee)
        {

            return wawserviceInstance.UpdateEmployeeDetail(employee);

        }


        [HttpPost]
        [Route("api/[controller]/UpdateEmployeeSteps")]
        public string UpdateEmployeeSteps(IEmployeeDetail employee)
        {
            return wawserviceInstance.UpdateEmployeeSteps(employee);
        }


        [HttpGet]
        [Route("api/[controller]/GetStepCountResult")]
        public List<IEmployeeStepsResult> GetStepCountResult(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate,string ResultType)
        {
            return wawserviceInstance.GetStepCountResult(Name, Email, Team, StartDate, EndDate, ResultType);
        }

        [HttpGet]
        [Route("api/[controller]/GetMissingEMployees")]
        public List<IMissingEmployees> GetMissingEMployees(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate)
        {
            return wawserviceInstance.GetMissingEMployees(Name, Email, Team, StartDate, EndDate);
        }


        [HttpPost]
        [Route("api/[controller]/SendEmail")]
        public string SendEmail(ISendEmail emaildata)
        { 
            string status = "";


            if (emaildata.EmailList.Count > 0)
            {

                status = wawserviceInstance.ThreadMail(null, null, emaildata.EmailList, emaildata.Subject, emaildata.EmailContent);
            }
            return status;
        }
    }
}
