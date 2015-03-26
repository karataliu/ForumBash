using System.Collections.Generic;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    public class User
    {
        public string Name { get; set; }

        public int IssueCount { get; set; }

        public IEnumerable<SOIssue> Issues { get; set; }
    }
}