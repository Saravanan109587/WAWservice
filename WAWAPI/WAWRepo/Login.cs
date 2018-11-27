using System;
using WAWEntity;
using Dapper;
using System.Data.SqlClient;
using System.Runtime;
using System.Data;
using System.Linq;

namespace WAWRepo
{
    public class Login:IDisposable
    { 

        public static SqlConnection connection;
        public Login(string connectionstring)
        {
            connection = new SqlConnection(connectionstring);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        public int AddLoginoutLog(ILoginoutLog LogDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", LogDetail.EmployeeId);
            parameters.Add("@Email", LogDetail.Email);
            parameters.Add("@Operation", LogDetail.Operation);
            int Result;

            try
            {

                 
                    Result = connection.Execute("usp_kmit_AddLoginOut", parameters, commandType: CommandType.StoredProcedure);
                 

                return Result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

      

        public string ValildateForLogin(string Email, string Password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", Email);
            parameters.Add("@password", Password);
            string Status = "";
            try
            { 
                    Status = connection.Query<string>("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
               
            }
            catch (Exception)
            {

                throw;
            }
            return Status;
        }

        public IEmployee GetEmployeeDetail(string Email)
        {
            var parameters = new DynamicParameters();
            IEmployee Employeedetail = new IEmployee();
            ITeam TeamDetail = new ITeam();
            parameters.Add("@Email", Email);

            try
            {
                
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    var QueryResult = connection.QueryMultiple("usp_kmit_GetEmployeeDetails", parameters, commandType: CommandType.StoredProcedure);
                Employeedetail = QueryResult.Read<IEmployee>().FirstOrDefault();
                TeamDetail = QueryResult.Read<ITeam>().FirstOrDefault();
                Employeedetail.Team= new ITeam();
                Employeedetail.Team.TeamName = TeamDetail.TeamName;
                Employeedetail.Team.TeamNumber = TeamDetail.TeamNumber;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return Employeedetail;
        }
    }
}
