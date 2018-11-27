using System;
using System.Collections.Generic;

namespace WAWEntity
{
    public class IEmployee
    {

        public int EmployeeId { get; set; }
        public ITeam Team { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UpdateId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Password { get; set; }
        public string IsAdminUser { get; set; }

    }

    public class IEmployeeDetail
    {
        public int? EmployeeId { get; set; }
        public int? StepCount { get; set; }
        public DateTime? DateOfStepCount { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? TeamNumber { get; set; }
        public string UpdateId { get; set; }
    }


    public class IEmployeeHistorySearchResult
    {

        public int EmployeeId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfStep { get; set; }
        public int Steps { get; set; }
        public int TeamNumber { get; set; }
        public DateTime? UpdateDate { get; set; }      
    }
    public class IEmployeeStepsResult
    {

        public int EmployeeId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }      
        public int TotalSteps { get; set; }
        public int NumberOfDay { get; set; }
        public decimal Average { get; set; }
        public int TeamNumber { get; set; }
        public int RankByTotalSteps { get; set; }
        public int RankByAverage { get; set; }

    }


    public class IEmployeeSearchResult
    {

        public int EmployeeId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }      
        public int TeamNumber { get; set; }
        public string IsAdminUser { get; set; }

    }


    public class IMissingEmployees
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime LastUpdatedStepdate { get; set; }

    }



    public class ILogIn
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }



    public class ILoginoutLog
    {
        public int? EmployeeId { get; set; }
        public string Email { get; set; }
        public string Operation { get; set; }

    }

    public class ITeam
    {
        public int? TeamNumber { get; set; }
        public string TeamName { get; set; }
        public int? EmployeeId { get; set; }

    }


    public class ISendEmail
    {
        public List<string> EmailList { get; set; }
        public string EmailContent { get; set; }
        public string Subject { get; set; }
    }
}
