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
       
        [Authorize]
        public ActionResult OwnerView()
        {
            ViewBag.Names = PullData();
            ViewBag.Message = GetAllRecords();

            return View();
        }

        public ActionResult FAQView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult ReportView()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<GeoLocation> PinList = db.GeoLocations.ToList();

            ViewBag.oneLine = TempData["oneLine"];
            ViewBag.absenteeind = TempData["absenteeind"];
            ViewBag.propclass = TempData["propclass"];
            ViewBag.propsubtype = TempData["propsubtype"];

            ViewBag.Locations = PinList;

            return View();
        }

        public ActionResult SearchData(string SearchFlag)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<datatable> AdList = db.datatables.Where(x => x.Ifsquat.ToUpper().Contains(SearchFlag.ToUpper())).ToList();

            ViewBag.Names = PullData();
            ViewBag.Message = AdList;

            return View("OwnerView");
        }

        public List<string> PullData()
        {
            SquatDBEntities db = new SquatDBEntities();

            return db.datatables.Select(x => x.Ifsquat).Distinct().ToList();
        }

        public List<datatable> GetAllRecords()
        {
            SquatDBEntities db = new SquatDBEntities();

            List<datatable> Records = db.datatables.ToList();

            return Records;
        }
        
        public ActionResult UnFlag(string UpdateName)
        {
            SquatDBEntities db = new SquatDBEntities();

            datatable FindItem = db.datatables.Find(UpdateName);

            return View("OwnerView", FindItem);
        }
        
        public ActionResult SaveItemUpdate(string UpdateName)
        {
                SquatDBEntities db = new SquatDBEntities();

                datatable FindItem = db.datatables.Find(UpdateName);
                string val = "";

                if (FindItem.Ifsquat.Trim() == "y")
                {
                   val = "n";
                }

                if (FindItem.Ifsquat.Trim() == "n")
                {
                    val = "y";
                }

                FindItem.Ifsquat = val;

                db.SaveChanges();

                return RedirectToAction("OwnerView", "Home");
        }

        public ActionResult SearchAddress(string AddressID)
        {
            SquatDBEntities db = new SquatDBEntities();

            List<datatable> AddressList = db.datatables.Where(x => x.dataaddress.ToUpper().Contains(AddressID.ToUpper())).ToList();

            ViewBag.Names = PullData();
            ViewBag.Message = AddressList;

            return View("OwnerView");
        }

    }
}