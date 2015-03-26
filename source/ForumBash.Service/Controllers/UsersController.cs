using System.Linq;
using System.Web.Http;
using System.Web.OData;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    [EnableQuery]
    public class UsersController : ODataController
    {
        private ForumBashContext context;

        public UsersController()
        {
            this.context = new ForumBashContext();
        }

        public IHttpActionResult Get()
        {
            var result = this.context.SOIssues
                .GroupBy(p => p.Owner ?? "unsigned")
                .Select(p => new User
                    {
                        Name = p.Key,
                        Issues = p
                    });

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Post(SOIssue account)
        {
            this.context.SOIssues.Add(account);
            this.context.SaveChanges();

            return Created(account);
        }
    }
}