using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Group1FinalProject.Controllers
{
    public class MAPController : Controller
    {
        // GET: MAP
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendData(finaltable coordinates)
        {
            string reported = "";


            coordinates.flagged = "y";
            coordinates.username = Convert.ToString(WindowsIdentity.GetCurrent().User);
            coordinates.datereported = DateTime.Now;
            coordinates.reportedtimes = 1;

            SquatDBEntities db = new SquatDBEntities();
            db.finaltables.Add(coordinates);

            try
            {
                
                db.SaveChanges();
        }
            catch
            {
                reported = "Already Reported!";
            }

            TempData["reported"] = reported;

            return View("../Home/ReportView");
        }

        public ActionResult PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> PinList = db.finaltables.ToList();

            ViewBag.Locations = PinList;

            return View("../Home/MapsView");
        }
    }
}