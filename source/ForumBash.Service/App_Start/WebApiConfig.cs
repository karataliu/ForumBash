using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using ODataOpenIssuesDashboard.Models;

namespace ForumBash.Service
{
    public static class WebApiConfig
    {
        private const string Namespace = "ForumBash";

        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("r1", null, GetModel());
        }

        private static IEdmModel GetModel()
        {
            var builder = new ODataConventionModelBuilder { Namespace = Namespace };
            var soissue = builder.EntityType<SOIssue>();
            soissue.Namespace = Namespace;
            soissue.Ignore(p => p.Type);
            soissue.Ignore(p => p.Status);
            soissue.Ignore(p => p.Tag);

            var user = builder.EntityType<User>();
            user.Namespace = Namespace;
            user.HasKey(p => p.Name);

            builder.EntitySet<SOIssue>("SOIssueSet");
            builder.EntitySet<User>("UserSet");
            return builder.GetEdmModel();
        }
    }
}
