using System.Collections.Generic;

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