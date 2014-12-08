using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ODataOpenIssuesDashboard.Utils;


namespace ODataOpenIssuesDashboard.Models
{
    public class ODataOpenIssues : DbContext
    {
        public DbSet<SOIssue> SOIssueList { get; set; }

        public ODataOpenIssues()
        {
            
        }

        public static List<SOIssue> GetAll()
        {

            using (ODataOpenIssues dbContext = new ODataOpenIssues())
            {
                var issue = from i in dbContext.SOIssueList
                            orderby i.CreationDate descending
                            select i;

                return issue.ToList();
            }
        }
    }
}