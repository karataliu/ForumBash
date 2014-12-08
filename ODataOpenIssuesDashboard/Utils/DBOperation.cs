using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODataOpenIssuesDashboard.Models;
using System.Configuration;

namespace ODataOpenIssuesDashboard.Utils
{
    public class DBOperation
    {
        public static void InsertDataToDB()
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["LogFilePath"];
                System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, true);
                file.WriteLine(DateTime.Now.ToString() + " Begin method [InsertDataToDB]");

                ODataOpenIssues dbContext = new ODataOpenIssues();

                DateTime to = DateTime.Now.Date;
                DateTime from = to.AddDays(-14);
                var client = new StackOverflowAPI.StackOverflowApiClient();

                //Questions with tag [odata]
                var questions = client.GetQuestionsAsync(from, to, "odata").Result.Items;

                foreach(var q in questions)
                {
                    SOIssue s = new SOIssue();
                    s.Id = q.QuestionId;
                    if(!dbContext.SOIssueList.Any(so => so.Id == s.Id))
                    {
                        var creationDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        creationDate = creationDate.AddSeconds(q.CreationDate).ToLocalTime();
                        
                        s.Title = q.Title;
                        s.CreationDate = creationDate;
                        s.URL = q.Link;
                        s.AnswerNumber = q.AnswerCount;
                        dbContext.SOIssueList.Add(s);
                    }
                }
                dbContext.SaveChanges();

                //Questions with tag [wcf-data-services]
                questions = client.GetQuestionsAsync(from, to, "wcf-data-services").Result.Items;

                foreach (var q in questions)
                {
                    SOIssue s = new SOIssue();
                    s.Id = q.QuestionId;
                    if (!dbContext.SOIssueList.Any(so => so.Id == s.Id))
                    {
                        var creationDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        creationDate = creationDate.AddSeconds(q.CreationDate).ToLocalTime();

                        s.Title = q.Title;
                        s.CreationDate = creationDate;
                        s.URL = q.Link;
                        s.AnswerNumber = q.AnswerCount;
                        dbContext.SOIssueList.Add(s);
                    }
                }


                dbContext.SaveChanges();

                file.WriteLine(DateTime.Now.ToString() + " End method [InsertDataToDB]");
                file.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void CheckDBUpdate()
        {
            try 
            {
                string logPath = ConfigurationManager.AppSettings["LogFilePath"];
                System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, true);
                file.WriteLine(DateTime.Now.ToString() + " Begin method [CheckDBUpdate]");

                ODataOpenIssues dbContext = new ODataOpenIssues();

                DateTime to = DateTime.Now.Date;
                DateTime from = to.AddDays(-14);
                var client = new StackOverflowAPI.StackOverflowApiClient();

                var questions = client.GetQuestionsAsync(from, to, "odata").Result.Items;
                foreach(var q in questions)
                {
                    var so = (from s in dbContext.SOIssueList
                              where s.Id == q.QuestionId
                              select s).First();
                    if (so.AnswerNumber != q.AnswerCount)
                        so.AnswerNumber = q.AnswerCount;

                    dbContext.SaveChanges();
                }

                questions = client.GetQuestionsAsync(from, to, "wcf-data-services").Result.Items;
                foreach (var q in questions)
                {
                    var so = (from s in dbContext.SOIssueList
                              where s.Id == q.QuestionId
                              select s).First();
                    if (so.AnswerNumber != q.AnswerCount)
                        so.AnswerNumber = q.AnswerCount;

                    dbContext.SaveChanges();
                }

                file.WriteLine(DateTime.Now.ToString() + " End method [CheckDBUpdate]");
                file.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static void UpdateOwner(int id, string owner)
        {
            if (owner != "")
            {
                using (ODataOpenIssues dbContext = new ODataOpenIssues())
                {
                    var q =
                        (from s in dbContext.SOIssueList
                         where s.Id == id
                         select s).First();
                    q.Owner = owner;
                    q.Status = Issue.IssueStatus.Assigned;
                    q.ResolvedDate = DateTime.Now;

                    dbContext.SaveChanges();
                }
            }
        }

        public static void UpdateStatus(int id, Issue.IssueStatus status)
        {
            using(ODataOpenIssues dbContext = new ODataOpenIssues())
            {
                var q =
                   (from s in dbContext.SOIssueList
                    where s.Id == id
                    select s).First();
                q.Status = status;

                if(status == Issue.IssueStatus.Active)
                {
                    q.Owner = null;
                }

                q.ResolvedDate = DateTime.Now;

                dbContext.SaveChanges();
            }
        }

        public static string GetOwner(int id)
        {
            using(ODataOpenIssues dbContext = new ODataOpenIssues())
            {
                var q =
                    (from s in dbContext.SOIssueList
                     where s.Id == id
                     select s).First();
                return q.Owner;
            }
        }

    }
}