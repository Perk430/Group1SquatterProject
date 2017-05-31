using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;

namespace Group1FinalProject.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportView()
        {
            //SquatDBEntities db = new SquatDBEntities();

            //List<finaltable> PinList = db.finaltables.ToList();

            ViewBag.Locations = PrintData();

            ViewBag.oneLine = TempData["oneLine"];
            ViewBag.absenteeind = TempData["absenteeind"];
            ViewBag.propclass = TempData["propclass"];
            ViewBag.propsubtype = TempData["propsubtype"];
            ViewBag.house = TempData["house"];
            ViewBag.wallType = TempData["wallType"];
            ViewBag.garagetype = TempData["garagetype"];

            ViewBag.Reported = TempData["reported"];

            return View();
        }

        public List<finaltable> PrintData()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> PinList = db.finaltables.ToList();

            return PinList;
        }

    }
}