using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Group1FinalProject.Controllers
{
    public class OwnerController : Controller
    {
        // GET: DB
        public ActionResult OwnerView()
        {
            ViewBag.AllData = GetAllRecords();
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View();
        }

        public List<finaltable> GetAllRecords()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> Records = db.finaltables.ToList();

            return Records;
        }

        public ActionResult SearchAddress(string AddressID)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> AddressList = db.finaltables.Where(x => x.address.ToUpper().Contains(AddressID.ToUpper())).ToList();

            ViewBag.AllData = AddressList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }
        public List<string> PullSearchFlagData()
        {
            SquatDBEntities db = new SquatDBEntities();

            return db.finaltables.Select(x => x.flagged).Distinct().ToList();
        }

        public List<string> PullSearchPoliceData()
        {
            SquatDBEntities db = new SquatDBEntities();

            return db.finaltables.Select(x => x.police).Distinct().ToList();
        }

        public ActionResult ReturnFlagData(string SearchFlag)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> FlagList = db.finaltables.Where(x => x.flagged.ToUpper().Contains(SearchFlag.ToUpper())).ToList();

            ViewBag.AllData = FlagList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }

        public ActionResult ReturnPoliceData(string SearchPolice)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> PoliceList = db.finaltables.Where(x => x.police.ToUpper().Contains(SearchPolice.ToUpper())).ToList();

            ViewBag.AllData = PoliceList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }

        public ActionResult DetailsView()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }

        public ActionResult AccountReportsView()
        {
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }

        public ActionResult MyReportsView()
        {
            ViewBag.Name = User.Identity.GetUserName();
            ViewBag.Data = GetAllMyRecords();

            return View();
        }

        public ActionResult UnFlag(string UpdateName)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(UpdateName);

            if (FindItem.username == User.Identity.GetUserId())
            {
                return View("MyReportsView", FindItem);
            }
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
            if(FindItem.username == User.Identity.GetUserId())
            {
                return RedirectToAction("MyReportsView");
            }
            else
            return RedirectToAction("OwnerView");
        }

        public ActionResult SaveComments(finaltable ToBeUpdated)
        {  
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated.address);

            FindItem.comments = ToBeUpdated.comments;

            db.SaveChanges();

            if (FindItem.username == User.Identity.GetUserId())
            {
                return RedirectToAction("MyReportsView");
            }
            else

            return RedirectToAction("OwnerView");

        }

        public ActionResult UpdateComments(string ToBeUpdated)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated);

            if(FindItem.username == User.Identity.GetUserId())
            {
                return View("AccountReportsView", FindItem);
            }
            else
          
            return View("DetailsView", FindItem);
        }

        public List<finaltable> GetAllMyRecords()
        {
            string loginusername = User.Identity.GetUserId();

            SquatDBEntities db = new SquatDBEntities();

            List<finaltable> Records = db.finaltables.Where(x => x.username == loginusername).ToList();

            return Records;
        }


        public ActionResult DeleteReport(string DeleteName)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable ToDelete = db.finaltables.Find(DeleteName);

            db.finaltables.Remove(ToDelete);

            db.SaveChanges();

            return RedirectToAction("MyReportsView", "Owner");
        }

        public ActionResult TickReport(string ReportCount)
        {
            SquatDBEntities db = new SquatDBEntities();

            finaltable ReportNumber = db.finaltables.Find(ReportCount);

            int count = Convert.ToInt32(ReportNumber.reportedtimes);

            int newcount = (count + 1);

            ReportNumber.reportedtimes = Convert.ToByte(newcount);

            db.SaveChanges();

            return RedirectToAction("OwnerView", "Owner");
        }

        public ActionResult SortDateDesc(string OrderValue)
        {
            SquatDBEntities db = new SquatDBEntities();
            if(OrderValue == "Desc")
            {
                List<finaltable> DateList = db.finaltables.OrderByDescending(x => x.datereported).ToList();
                ViewBag.AllData = DateList;
            }
            if (OrderValue == "Asc")
            {
                List<finaltable> DateList = db.finaltables.OrderBy(x => x.datereported).ToList();
                ViewBag.AllData = DateList;
            }

            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }
    }
}