using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODataOpenIssuesDashboard.Models;
using PagedList;
using ODataOpenIssuesDashboard.Utils;

namespace ODataOpenIssuesDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? All, bool? Assigned, bool? NotResolved, bool? Active, int? page, string searchString)
        {
            var pageNumber = page ?? 1;
            int pageSize = 20;

            ViewBag.All = All;
            ViewBag.Assigned = Assigned;
            ViewBag.NotResolved = NotResolved;
            ViewBag.Active = Active;

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Resolved",
                Value = "Resolved"
            });
            items.Add(new SelectListItem
            {
                Text = "Closed",
                Value = "Closed"
            });
            items.Add(new SelectListItem
            {
                Text = "Active",
                Value = "Active"
            });
            items.Add(new SelectListItem
            {
                Text = "Irrelevant",
                Value = "Irrelevant"
            });
            
            ViewBag.StatusChoice = items;

            var issues = ODataOpenIssues.GetAll();

            if(!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(i => (i.Owner != null) && ( i.Owner.ToUpper().Contains(searchString.ToUpper()))).ToList();
                All = true;
            }

            if (All == true)
            {
                var model = issues.ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else if (Assigned == true)
            {
                var model = issues.Where(i => i.Status == Issue.IssueStatus.Assigned).ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else if (NotResolved == true)
            {
                var model = issues.Where(i => i.Status != Issue.IssueStatus.Closed && i.Status != Issue.IssueStatus.Resolved && i.Status != Issue.IssueStatus.Irrelevant).ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else if (Active == true)
            {
                var model = issues.Where(i => i.Status == Issue.IssueStatus.Active).ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else
            {
                var model = issues.Where(i => i.Status != Issue.IssueStatus.Closed && i.Status != Issue.IssueStatus.Resolved && i.Status != Issue.IssueStatus.Irrelevant).ToPagedList(pageNumber, pageSize);
                return View(model);
            }

        }


        public ActionResult IndexForm(string owner, string operation, int id)
        {

            switch (operation)
            {
                case "Resolved":
                    DBOperation.UpdateStatus(id, Issue.IssueStatus.Resolved);
                    break;
                case "Closed":
                    DBOperation.UpdateStatus(id, Issue.IssueStatus.Closed);
                    break;
                case "Active":
                    DBOperation.UpdateStatus(id, Issue.IssueStatus.Active);
                    break;
                case "Irrelevant":
                    DBOperation.UpdateStatus(id, Issue.IssueStatus.Irrelevant);
                    break;
                default:
                    break;
            }

            string dbOwner = DBOperation.GetOwner(id);
            if (operation == "" && owner != dbOwner)
            {
                DBOperation.UpdateOwner(id, owner);
            }

            return RedirectToAction("Index");
        }
    }
}