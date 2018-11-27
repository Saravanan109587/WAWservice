using System;
using WAWRepo;
    using WAWEntity;
namespace WAWBusiness
{
    public class LoginBusinessLogics
    {
        Login repoInstance;
        public LoginBusinessLogics(string connectionstring)
        {
            repoInstance = new Login(connectionstring);
        }


        public int AddLoginoutLog(ILoginoutLog LogDetail)
        {
            return repoInstance.AddLoginoutLog(LogDetail);
        }

        public string ValildateForLogin(string Email, string Password)
        {
            return repoInstance.ValildateForLogin(Email, Password);
        }

        public IEmployee GetEmployeeDetail(string Email)
        {
          
            return repoInstance.GetEmployeeDetail(Email);
        }
    }
}
