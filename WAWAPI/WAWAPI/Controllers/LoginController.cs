using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WAWBusiness;
using WAWEntity;


namespace WAWAPi.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        LoginBusinessLogics blInstance;
        public LoginController(IConfiguration con)
        {
            _configuration = con;
            blInstance = new LoginBusinessLogics(_configuration["ConnectionString"]);

        }
        
        [HttpPost]
        [Route("api/[controller]/AddLoginoutLog")]
        public int AddLoginoutLog(ILoginoutLog LogDetail)
        {
            return blInstance.AddLoginoutLog(LogDetail);
        }

        [HttpGet]
        [Route("api/[controller]/ValildateForLogin")]
        public string ValildateForLogin(string Email, string Password)
        {
            return blInstance.ValildateForLogin(Email, Password);
        }

        [HttpGet]
        [Route("api/[controller]/GetEmployeeDetail")]
        public IEmployee GetEmployeeDetail(string Email)
        {

            return blInstance.GetEmployeeDetail(Email);
        }

    }

}
