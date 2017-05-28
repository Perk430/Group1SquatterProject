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
        // GET: DB
        public ActionResult OwnerView()
        {
            ViewBag.SearchItems = PullData();
            ViewBag.Data = GetAllRecords();

            return View();
        }

        public ActionResult DetailsView()
        {
            return View();
        }

        public List<string> PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            return db.finaltables.Select(x => x.flagged).Distinct().ToList();
        }

        public List<finaltable> GetAllRecords()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> Records = db.finaltables.ToList();

            return Records;
        }

        public ActionResult SearchData(string SearchFlag)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> AdList = db.finaltables.Where(x => x.flagged.ToUpper().Contains(SearchFlag.ToUpper())).ToList();

            ViewBag.Names = PullData();
            ViewBag.Message = AdList;

            return View("OwnerView");
        }

        public ActionResult UnFlag(string UpdateName)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(UpdateName);

            return View("OwnerView", FindItem);
        }

        public ActionResult SaveFlag(string flagname)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(flagname);
            string val = "";

            if (FindItem.flagged.Trim() == "y")
            {
                val = "n";
            }

            if (FindItem.flagged.Trim() == "n")
            {
                val = "y";
            }

            FindItem.flagged = val;

            db.SaveChanges();

            return RedirectToAction("OwnerView");
        }

        public ActionResult SearchAddress(string AddressID)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> AddressList = db.finaltables.Where(x => x.address.ToUpper().Contains(AddressID.ToUpper())).ToList();

            ViewBag.Names = PullData();
            ViewBag.Message = AddressList;

            return View("OwnerView");
        }

        public ActionResult SaveComments(finaltable ToBeUpdated)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated.address);

            FindItem.comments = ToBeUpdated.comments;

            db.SaveChanges();

            return RedirectToAction("OwnerView");

        }

        public ActionResult UpdateComments(string ToBeUpdated)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated);

            return View("DetailsView", FindItem);
        }

    }
}