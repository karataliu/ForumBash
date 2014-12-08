using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataOpenIssuesDashboard.Models
{
    public class ResourcesList<T>
    {
        public bool HasMore
        {
            get;
            set;
        }

        public IEnumerable<T> Items
        {
            get;
            set;
        }
    }
}