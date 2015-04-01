using System.Web.Mvc;

namespace ForumBash.Service
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "ForumBash - Dashboard";

            return View();
        }
    }
}