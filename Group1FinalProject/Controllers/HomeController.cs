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

        public ActionResult APIView()
        { 

            return View();
        }

        public ActionResult DBView()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<Address> AdList = db.Addresses.ToList();

            ViewBag.Message = AdList;

            return View();
        }

        public ActionResult FAQView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MapsView()
        {

            return View();
        }
    }
}