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

        public ActionResult SendData(datatable coordinates)
        {
            Random rdn = new Random();
            int randomNumber = rdn.Next(0, 5000);

            string name = Convert.ToString(randomNumber);
                       
            coordinates.Ifsquat = "y";
            coordinates.username = "Test" + name;

            SquatDBEntities db = new SquatDBEntities();

            db.datatables.Add(coordinates);

            db.SaveChanges();

            return View("../Home/MapsView");
        }

        public ActionResult PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<datatable> PinList = db.datatables.ToList();

            ViewBag.Locations = PinList;

            return View("../Home/MapsView");
        }
    }
}