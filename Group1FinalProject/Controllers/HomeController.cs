﻿using System;
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

        public ActionResult APIView()
        { 

            return View();
        }

        public ActionResult DBView()
        {
            return RedirectToAction("../DB/PullData");
        }

        public ActionResult FAQView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ReportView()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<GeoLocation> PinList = db.GeoLocations.ToList();

            ViewBag.oneLine = TempData["oneLine"];
            ViewBag.absenteeind = TempData["absenteeind"];
            ViewBag.propclass = TempData["propclass"];
            ViewBag.propsubtype = TempData["propsubtype"];

            ViewBag.Locations = PinList;

            return View();
        }
    }
}