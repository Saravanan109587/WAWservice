using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using WAWEntity;
using WAWRepo;

namespace WAWBusiness
{
    public class WAWBusinessLogics
    {
        string Conection = "";
        public WAW repoInstance;
        public WAWBusinessLogics(string Connectionstring)
        {
            Conection = Connectionstring;
            repoInstance = new WAW(Connectionstring);
        }

        public string AddEmployee(IEmployee employee)
        {
            return repoInstance.AddEmployee(employee);
        }

        public List<IEmployee> getAdminUsers()
        {
            return repoInstance.getAdminUsers();
        }

        public string AddStepCount(IEmployeeDetail employee)
        {
            return repoInstance.AddStepCount(employee);

        }

        public List<IEmployeeDetail> GetEmployeeStepsbyId(string Email, int EmpId)
        {
            return repoInstance.GetEmployeeStepsbyId(Email, EmpId);
        }


        public List<string> GetEmailForReminder()
        {
            List<string> EmailList = new List<string>();

            return repoInstance.GetEmailForReminder();

        }

        public List<ITeam> GetAllTeams()
        {
           
            return repoInstance.GetAllTeams();
        }


        public List<IEmployeeHistorySearchResult> SearchEmployeeHistoryFindAll(string Name, string Email, int Team,DateTime StartDate,DateTime EndDate)
        {
            return repoInstance.SearchEmployeeHistoryFindAll(Name, Email, Team, StartDate, EndDate);
        }

        public List<IEmployeeSearchResult> SearchEmployeeFindAll(string Name, string Email, int Team)
        {
            return repoInstance.SearchEmployeeFindAll(Name, Email, Team);
        }
        public string UpdateEmployeeDetail(IEmployee employee)
        {

            return repoInstance.UpdateEmployeeDetail(employee);
        }

        public string UpdateEmployeeSteps(IEmployeeDetail employee)
        {

            return repoInstance.UpdateEmployeeSteps(employee);

        }

        public List<IEmployeeStepsResult> GetStepCountResult(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate,string ResultType)
        {
            return repoInstance.GetStepCountResult(Name, Email, Team, StartDate, EndDate, ResultType);
        }
        public List<IMissingEmployees> GetMissingEMployees(string Name, string Email, int Team, DateTime StartDate, DateTime EndDate)
        {
            return repoInstance.GetMissingEMployees(Name, Email, Team, StartDate, EndDate);
        }


        public string ThreadMail(List<byte[]> Attachment, List<string> AttachmentFileName, List<string> emailList, string Subject, string body)
        {
         
            if(File.Exists(@"Files\" + body + ".html"))
            {
                using (StreamReader reader = new StreamReader(@"Files\" + body + ".html"))
                {
                    body = reader.ReadToEnd();
                }

            }
            body = body.Replace("{Date}","("+ DateTime.Now.ToString("MM/dd/yyyy")+")");
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var Msg = new MailMessage();                   
                    foreach (string email in emailList)
                    {
                        Msg.To.Add(email);
                    }

                    Msg.Subject = Subject;

                    if (body != null)
                    {
                        Msg.Body += body;
                    }
                    else
                    {
                        Msg.Body += "<h1><b>It seems like you have't Update Todays's Step Count,So Please Update your step Count for Today " + "(" + DateTime.Now.ToString("MM/dd/yyyy") + ")</b></h1>";

                    }

                    //Msg.Attachments.Add(new Attachment(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/e.pdf")));
                    if (AttachmentFileName != null && AttachmentFileName.Count > 0)
                    {
                        for (var i = 0; i < Attachment.Count; i++)
                        {
                            Msg.Attachments.Add(new Attachment(new MemoryStream(Attachment[i]), AttachmentFileName[i]));
                        }
                    }

                    Msg.IsBodyHtml = true;
                    var smtp = new SmtpClient();
                    Msg.From = new MailAddress("info@kmitsolutions.com");
                    Msg.CC.Add("info@kmitsolutions.com");

                    //smtp.Host = Host;
                    //smtp.EnableSsl = EnableSSL;
                    //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    //NetworkCred.UserName = UserName;
                    //NetworkCred.Password = Password;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = NetworkCred;
                    //smtp.Port = Convert.ToInt32(Port);

                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "info@kmitsolutions.com";
                    NetworkCred.Password = "cpnsjliiciesthyz";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(Msg);


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }
    }
}
