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

        public ActionResult SendData(GeoLocation coordinates)
        {
            SquatDBEntities db = new SquatDBEntities();

            db.GeoLocations.Add(coordinates);

            db.SaveChanges();

            return View("../Home/MapsView");
        }

        public ActionResult PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<GeoLocation> PinList = db.GeoLocations.ToList();

            ViewBag.Locations = PinList;

            return View("../Home/MapsView");
        }
    }
}