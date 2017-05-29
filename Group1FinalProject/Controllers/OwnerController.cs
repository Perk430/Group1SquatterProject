using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;
using System.Security.Principal;

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

        public ActionResult AccountReportsView()
        {
            return View();
        }

        public ActionResult MyReportsView()
        {
            ViewBag.Data = GetAllMyRecords();

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

            string loginusername = Convert.ToString(WindowsIdentity.GetCurrent().User);
            if (FindItem.username == loginusername)
            {
                return View("MyReportsView", FindItem);
            }
            return View("OwnerView", FindItem);
        }

        public ActionResult SaveFlag(string flagname)
        {
            SquatDBEntities db = new SquatDBEntities();

            string loginusername = Convert.ToString(WindowsIdentity.GetCurrent().User);

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
            if(FindItem.username == loginusername)
            {
                return RedirectToAction("MyReportsView");
            }
            else
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
            string loginusername = Convert.ToString(WindowsIdentity.GetCurrent().User);

            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated.address);

            FindItem.comments = ToBeUpdated.comments;

            db.SaveChanges();

            if (FindItem.username == loginusername)
            {
                return RedirectToAction("MyReportsView");
            }
            else

            return RedirectToAction("OwnerView");

        }

        public ActionResult UpdateComments(string ToBeUpdated)
        {
            string loginusername = Convert.ToString(WindowsIdentity.GetCurrent().User);
            
            SquatDBEntities db = new SquatDBEntities();

            finaltable FindItem = db.finaltables.Find(ToBeUpdated);

            if(FindItem.username == loginusername)
            {
                return View("AccountReportsView", FindItem);
            }
            else
          
            return View("DetailsView", FindItem);
        }

        public List<finaltable> GetAllMyRecords()
        {
            string loginusername = Convert.ToString(WindowsIdentity.GetCurrent().User);

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
    }
}