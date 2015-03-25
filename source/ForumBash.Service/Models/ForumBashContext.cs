using System.Data.Entity;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    internal class ForumBashContext : DbContext
    {
        public ForumBashContext() : base("ODataOpenIssues") { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(p => p.Name);
        }

        public virtual DbSet<SOIssue> SOIssues { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}