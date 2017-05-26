using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;

namespace Group1FinalProject.Controllers
{
    public class OwnerController : Controller
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

            List<datatable> AdList = db.datatables.ToList();

            TempData["AdList"] = AdList;

            return View("../Home/OwnerView");
        }

        public ActionResult SendData(datatable Input)
        {
            db.datatables.Add(Input);

            db.SaveChanges();

            return RedirectToAction ("PullData");
        }

        public ActionResult DeleteData(string DeleteName)
        {
            datatable ToDelete = db.datatables.Find(DeleteName);

            db.datatables.Remove(ToDelete);

            db.SaveChanges();

            return RedirectToAction("PullData");
        }

        public ActionResult SearchData(string SearchFlag)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<datatable> AdList = db.datatables.Where(x => x.Ifsquat.ToUpper().Contains(SearchFlag.ToUpper())).ToList();

            ViewBag.Names = AdList;

            return RedirectToAction("PullData");
        }

    }
}