using System.Web.Mvc;

namespace ForumBash.Service
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "ForumBash - Dashboard";
            var model = new BashModelHome { CurrentView = "srt111", Status = "Nrt"};
            return View(model);
        }

        public new ActionResult User()
        {
            ViewBag.Title = "ForumBash - User";
            return View();
        }
    }
}