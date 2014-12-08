using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataOpenIssuesDashboard.Models
{
    public class Issue
    {
        public enum IssueType
        {
            StackOverflow = 0
        };

        public enum IssueStatus
        {
            Active = 0,
            Assigned = 1,
            Resolved = 2,
            Closed = 3,
            Irrelevant = 4
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public String Title { get; set; }
        public IssueType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsResponded { get; set; }
        public bool IsSolved { get; set; }
        public DateTime ? ResolvedDate { get; set; }
        public String Owner { get; set; }

        public IssueStatus Status { get; set; }
    }
}