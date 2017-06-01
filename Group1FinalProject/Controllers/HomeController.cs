using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;
using Microsoft.AspNet.Identity;

namespace Group1FinalProject.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }
        [Authorize]
        public ActionResult OwnerView()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }

        public ActionResult FAQView()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }
        [Authorize]
        public ActionResult ReportView()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }

        public ActionResult DetailsView()
        {
            ViewBag.Name = User.Identity.GetUserName();
            return View();
        }
        public ActionResult AboutUs()
        {

            ViewBag.Name = User.Identity.GetUserName();
            return View();
        }

    }
}