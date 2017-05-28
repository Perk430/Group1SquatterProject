using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;

namespace Group1FinalProject.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
       
        //[Authorize]
        public ActionResult OwnerView()
        {
            return View();
        }

        public ActionResult FAQView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[Authorize]
        public ActionResult ReportView()
        {
                SquatDBEntities db = new SquatDBEntities();

                List<finaltable> PinList = db.finaltables.ToList();

                ViewBag.Locations = PinList;

                ViewBag.oneLine = TempData["oneLine"];
                ViewBag.absenteeind = TempData["absenteeind"];
                ViewBag.propclass = TempData["propclass"];
                ViewBag.propsubtype = TempData["propsubtype"];
                ViewBag.house = TempData["house"];

                ViewBag.Reported = TempData["reported"];

                return View();
        }

        public ActionResult DetailsView()
        {
            return View();
        }

    }
}