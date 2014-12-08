using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataOpenIssuesDashboard.Models
{
    public class IssuesViewModel
    {
        public List<SOIssue> SOIssues { get; set; }
        public List<ExchangeIssue> ExchangeIssues { get; set; }

        public IssuesViewModel(List<SOIssue> soIssue, List<ExchangeIssue> exchangeIssue)
        {
            SOIssues = soIssue;
            ExchangeIssues = exchangeIssue;
        }
    }
}