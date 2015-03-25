using System;
using System.Collections.Generic;

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