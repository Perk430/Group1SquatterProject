using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;

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

            Random rdn = new Random();
            int randomNumber = rdn.Next(0, 5000);

            string name = Convert.ToString(randomNumber);

            coordinates.flagged = "y";
            coordinates.username = "Test" + name;
            coordinates.comments = "Tests comments";
            coordinates.timeofday = "9:00 to 5:00 PM";
            coordinates.narc = "x";
            coordinates.startsquat = DateTime.Parse("04/19/2016");
            coordinates.police = "o";
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