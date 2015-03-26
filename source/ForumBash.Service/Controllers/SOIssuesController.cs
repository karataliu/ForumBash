using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    [EnableQuery]
    public class SOIssuesController : ODataController
    {
        private static readonly string[] AllowedPatchProperties = { "Owner", "Status" };

        private ForumBashContext context;

        public SOIssuesController()
        {
            this.context = new ForumBashContext();
        }

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            var result = this.context.SOIssues;

            return Ok(result);
        }

        public IHttpActionResult Get(int key)
        {
            var issue = this.context.SOIssues.SingleOrDefault(s => s.Id == key);

            if (issue == null)
            {
                return NotFound();
            }

            return Ok(issue);
        }

        public IHttpActionResult Patch([FromODataUri] int key, [FromBody] Delta<SOIssue> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!patch.GetChangedPropertyNames().All(n => AllowedPatchProperties.Any(a => a == n)))
            {
                return BadRequest("Only Owner and Status are allowed to be patched.");
            }

            var issue = this.context.SOIssues.SingleOrDefault(s => s.Id == key);
            if (issue == null)
            {
                return NotFound();
            }

            patch.Patch(issue);
            context.SaveChanges();

            return Updated(issue);
        }
    }
}