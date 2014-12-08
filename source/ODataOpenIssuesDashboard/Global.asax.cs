using ODataOpenIssuesDashboard.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ODataOpenIssuesDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Task.Factory.StartNew(UpdateDataTimely);
        }

        protected void UpdateDataTimely()
        {
            string logPath = ConfigurationManager.AppSettings["LogFilePath"];
            try
            {
                DBOperation.InsertDataToDB();
                DBOperation.CheckDBUpdate();
            }
            catch(Exception e)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, true);
                file.WriteLine("Exception at: " + DateTime.Now.ToString());
                file.WriteLine(e.Message);
                file.Close();
            }
            finally
            {
                Task.Delay(1000 * 60 * 20).ContinueWith(_ =>
                {
                    UpdateDataTimely();
                });
            }
        }
    }
}
