using System.Web.Http;
using System.Web.OData;

namespace ForumBash.Service
{
    [EnableQuery]
    public class SOIssuesController : ODataController
    {
        private ForumBashContext context;

        public SOIssuesController()
        {
            this.context = new ForumBashContext();
        }

        public IHttpActionResult Get()
        {
            var result = this.context.SOIssues;

            return Ok(result);
        }

    }
}