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
        SquatDBEntities db = new SquatDBEntities();
        // GET: DB
        public ActionResult OwnerView()
        {
            //main action that sends all data to the ownerview by default
            ViewBag.Name = User.Identity.GetUserName();

            ViewBag.AllData = GetAllRecords();
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View();
        }
        //method called to grab data from the db
        public List<finaltable> GetAllRecords()
        {
            List<finaltable> Records = db.finaltables.ToList();

            return Records;
        }
        //action that grabs the id from the url string and creates a new distinct list using lambda expression
        public ActionResult SearchAddress(string AddressID)
        {
            List<finaltable> AddressList = db.finaltables.Where(x => x.address.ToUpper().Contains(AddressID.ToUpper())).ToList();

            ViewBag.AllData = AddressList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }
        public List<string> PullSearchFlagData()
        {
            //returns distinct list allowing us to search on the flag status
            return db.finaltables.Select(x => x.flagged).Distinct().ToList();
        }

        public List<string> PullSearchPoliceData()
        {
            //similar list allowing us to search on police data
            return db.finaltables.Select(x => x.police).Distinct().ToList();
        }

        public ActionResult ReturnFlagData(string SearchFlag)
        {
            //the action that takes the search value allowing to to return a new list
            List<finaltable> FlagList = db.finaltables.Where(x => x.flagged.ToUpper().Contains(SearchFlag.ToUpper())).ToList();

            ViewBag.AllData = FlagList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }

        public ActionResult ReturnPoliceData(string SearchPolice)
        {
            
            List<finaltable> PoliceList = db.finaltables.Where(x => x.police.ToUpper().Contains(SearchPolice.ToUpper())).ToList();

            ViewBag.AllData = PoliceList;
            ViewBag.SearchFlagData = PullSearchFlagData();
            ViewBag.SearchPoliceData = PullSearchPoliceData();

            return View("OwnerView");
        }

        public ActionResult DetailsView()
        {
            //sends user id to the details view and allows us to display details on selected report
            ViewBag.Name = User.Identity.GetUserName();

            return View();
        }

        public ActionResult AccountReportsView()
        {
            //sends user id to the accounts report view and allows us to display details on selected report
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
            //allows take the url value and fing the correct item to flag
            finaltable FindItem = db.finaltables.Find(UpdateName);

            if (FindItem.username == User.Identity.GetUserId())
            {
                return View("MyReportsView", FindItem);
            }
            return View("OwnerView", FindItem);
        }

        public ActionResult SaveFlag(string flagname)
        {
            //changes the flag value in the owner view and saves changes
            finaltable FindItem = db.finaltables.Find(flagname);

            if (FindItem.flagged == "Y")
            {
                FindItem.flagged = "N";
            }
            else
            {
                FindItem.flagged = "Y";
            }
          
            db.SaveChanges();

            return RedirectToAction("OwnerView");
        }


        public ActionResult SaveMyFlag(string flagname)
        {
            //changes the flag value in the myreports view and saves changes
            finaltable FindItem = db.finaltables.Find(flagname);

            if (FindItem.flagged == "Y")
            {
                FindItem.flagged = "N";
            }
            else
            {
                FindItem.flagged = "Y";
            }

            db.SaveChanges();
         
            return RedirectToAction("MyReportsView");
                        
        }

        public ActionResult SaveComments(finaltable ToBeUpdated)
        {
            //allows us to save changes made to the comments field and limit access to non owners
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
            //allows us to update changes made to the comments field and limit access to non owners
            ViewBag.Name = User.Identity.GetUserName();

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
            //grabs all records that match user identity in the myreportsview
            string loginusername = User.Identity.GetUserId();

            List<finaltable> Records = db.finaltables.Where(x => x.username == loginusername).ToList();

            return Records;
        }


        public ActionResult DeleteReport(string DeleteName)
        {
            //action that allows us to delete reports
            finaltable ToDelete = db.finaltables.Find(DeleteName);

            db.finaltables.Remove(ToDelete);

            db.SaveChanges();

            return RedirectToAction("MyReportsView", "Owner");
        }

        public ActionResult TickReport(string ReportCount)
        {
            //action that allows us to add a value to the times reported
            finaltable ReportNumber = db.finaltables.Find(ReportCount);

            int count = Convert.ToInt32(ReportNumber.reportedtimes);

            int newcount = (count + 1);

            ReportNumber.reportedtimes = Convert.ToByte(newcount);

            db.SaveChanges();

            return RedirectToAction("OwnerView", "Owner");
        }

        public ActionResult SortDateDesc(string OrderValue)
        {
            //action that allows us to search in ascending and descending values by date
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