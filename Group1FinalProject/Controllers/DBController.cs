using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;

namespace Group1FinalProject.Controllers
{
    public class DBController : Controller
    {
        // GET: DB
        public ActionResult DB()
        {
            return View();
        }

        public ActionResult PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<Address> AdList = db.Addresses.ToList();

            ViewBag.Message = AdList;

            return View("DBView");
        }

        public ActionResult SendData(Address Input)
        {
            SquatDBEntities db = new SquatDBEntities();

            db.Addresses.Add(Input);

            db.SaveChanges();

            return RedirectToAction ("../Home/DBView");
        }
    }
}