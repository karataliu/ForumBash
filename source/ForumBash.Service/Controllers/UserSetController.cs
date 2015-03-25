using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace ForumBash.Service.Controllers
{
    public class UserSetController : ODataController
    {
        private ForumBashContext context;

        public UserSetController()
        {
            this.context = new ForumBashContext();
        }

        public IHttpActionResult Get()
        {
            var result = this.context.Users;

            return Ok(result);
        }
    }
}