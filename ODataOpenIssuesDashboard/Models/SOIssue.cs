using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ODataOpenIssuesDashboard.Models
{
    public class SOIssue : Issue 
    {
        public List<string> Tag { get; set; }
        public String URL { get; set; }
        public int AnswerNumber { get; set; }
        public int CommentNumber { get; set; }

        public SOIssue()
        {
            Type = Issue.IssueType.StackOverflow;
        }

        

    }
}