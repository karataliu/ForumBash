using System.Web.Http;
using System.Web.OData;

namespace ForumBash.Service.Controllers
{
    public class SOIssueSetController : ODataController
    {
        private ForumBashContext context;

        public SOIssueSetController()
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