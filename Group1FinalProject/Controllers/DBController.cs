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
        SquatDBEntities db = new SquatDBEntities();
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

            return View("../Home/OwnerView");
        }

        public ActionResult SendData(Address Input)
        {
            db.Addresses.Add(Input);

            db.SaveChanges();

            return RedirectToAction ("PullData");
        }

        public ActionResult DeleteData(string DeleteName)
        {
            Address ToDelete = db.Addresses.Find(DeleteName);

            db.Addresses.Remove(ToDelete);

            db.SaveChanges();

            return RedirectToAction("PullData");
        }
    }
}