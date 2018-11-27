using System;
using System.Collections.Generic;
using System.Text;
using WAWEntity;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WAWRepo
{
    public class WAW
    {

        string connection = "";
        public WAW(string Connectionstring)
        {
            connection = Connectionstring;
        }

        public string AddEmployee(IEmployee employee)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Name", employee.Name);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@Password", employee.Password);
            parameters.Add("@updateId", employee.UpdateId);
            parameters.Add("@team", employee.Team.TeamNumber);

            try
            {
                string result;
                using (var con = new SqlConnection(connection))
                {
                    result = con.Query<string>("kmit_Insert_Employee", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

                }

                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }


        public List<IEmployee> getAdminUsers()
        {
            List<IEmployee> adminUsers;
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    adminUsers = con.Query<IEmployee>("kmit_GetAdminUsers", commandType: CommandType.StoredProcedure).ToList<IEmployee>();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return adminUsers;
        }


        public string AddStepCount(IEmployeeDetail employee)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeId", employee.EmployeeId);
            parameters.Add("@Date", employee.DateOfStepCount);
            parameters.Add("@stepCount", employee.StepCount);
            parameters.Add("@UpdateId", employee.UpdateId);


            try
            {
                string result;
                using (var con = new SqlConnection(connection))
                {
                    result = con.Query<string>("kmit_InsertSteps", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

                }


                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }


        public List<IEmployeeDetail> GetEmployeeStepsbyId(string Email, int EmpId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", EmpId);
            parameters.Add("@Email", Email);
            List<IEmployeeDetail> stephistory;

            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    stephistory = con.Query<IEmployeeDetail>("usp_Kmit_GetEmployeeStepsbyId", parameters, commandType: CommandType.StoredProcedure).ToList<IEmployeeDetail>();


                }
            }
            catch (Exception e)
            {

                throw;
            }


            return stephistory;
        }

        public List<string>  GetEmailForReminder()
        {
            List<string> EmailList = new List<string>();

            try
            {

                using (var con = new SqlConnection(connection))
                {
                    EmailList = con.Query<string>("Usp_Kmit_GetEmailForReminder", commandType: CommandType.StoredProcedure).ToList<string>();

                }

                
                return EmailList;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public List<ITeam> GetAllTeams()
        {
            List<ITeam> TeamList;
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    TeamList = con.Query<ITeam>("usp_Eps_Team_FindAll", commandType: CommandType.StoredProcedure).ToList<ITeam>();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return TeamList;
        }


        public List<IEmployeeHistorySearchResult> SearchEmployeeHistoryFindAll(string Name, string Email, int Team,DateTime StartDate,DateTime EndDate)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Name", Name);
            parameters.Add("@Email", Email);
            parameters.Add("@Team", Team);
            parameters.Add("@StartDate", StartDate);
            parameters.Add("@EndDate", EndDate);

            List<IEmployeeHistorySearchResult> SearchResult;
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    SearchResult = con.Query<IEmployeeHistorySearchResult>("Usp_kmit_SearchEmployeeHistoryFindAll", parameters, commandType: CommandType.StoredProcedure).ToList<IEmployeeHistorySearchResult>();
                     
                }
            }
             

            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return SearchResult;

        }



        public List<IEmployeeSearchResult> SearchEmployeeFindAll(string Name, string Email, int Team )
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Name", Name);
            parameters.Add("@Email", Email);
            parameters.Add("@Team", Team);

            List<IEmployeeSearchResult> SearchResult;
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    SearchResult = con.Query<IEmployeeSearchResult>("[Usp_kmit_EmployeeSearch_FindAll]", parameters, commandType: CommandType.StoredProcedure).ToList<IEmployeeSearchResult>();

                }
            }


            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return SearchResult;

        }
        public string UpdateEmployeeDetail(IEmployee employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", employee.Name);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@Team", employee.Team.TeamNumber);
            parameters.Add("@EmployeeId", employee.EmployeeId);
            parameters.Add("@IsAdminUser", employee.IsAdminUser);

            string result = "";
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    result = con.Query<string>("[Usp_Kmit_UpdateEmployee]", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

                }
            }


            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return result;

        }


        public string UpdateEmployeeSteps(IEmployeeDetail employee)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Date", employee.DateOfStepCount);
            parameters.Add("@UpdateId", employee.UpdateId);
            parameters.Add("@Steps", employee.StepCount);
            parameters.Add("@EmployeeId", employee.EmployeeId);

            int count; 
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                      count = con.Execute("[Usp_kmit_UpdateEmployeeSteps]", parameters, commandType: CommandType.StoredProcedure);

                }
            }


            catch (Exception e)
            {

                throw new Exception(e.Message);
            }



            return count > 0 ? "Success" : "Failted";

        }



        public List<IEmployeeStepsResult> GetStepCountResult(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate,string ResultType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", Name);
            parameters.Add("@Email", Email);
            parameters.Add("@Team", Team);
            parameters.Add("@StartDate", StartDate);
            parameters.Add("@EndDate", EndDate);
            parameters.Add("@ResultType", ResultType);
             
            List<IEmployeeStepsResult> result;
           
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    result = con.Query<IEmployeeStepsResult>("[Usp_kmit_StepCountResult]", parameters, commandType: CommandType.StoredProcedure).ToList<IEmployeeStepsResult>();

                }
            }


            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return result;
        }


        public List<IMissingEmployees> GetMissingEMployees(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", Name);
            parameters.Add("@Email", Email);
            parameters.Add("@Team", Team);
            parameters.Add("@StartDate", StartDate);
            parameters.Add("@EndDate", EndDate);

            List<IMissingEmployees> result;

            try
            {
                using (var con = new SqlConnection(connection))
                {
                    //con.Execute("kmit_ValidateForLogin", parameters, commandType: CommandType.StoredProcedure);
                    result = con.Query<IMissingEmployees>("[Usp_Kmit_GetNotStepEnteredEmployee]", parameters, commandType: CommandType.StoredProcedure).ToList<IMissingEmployees>();

                }
            }


            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return result;
        }


    }
}
