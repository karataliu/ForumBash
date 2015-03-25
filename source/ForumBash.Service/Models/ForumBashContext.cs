using System.Data.Entity;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    internal class ForumBashContext : DbContext
    {
        public ForumBashContext() : base("ODataOpenIssues") { }

        public virtual DbSet<SOIssue> SOIssues { get; set; }

    }
}