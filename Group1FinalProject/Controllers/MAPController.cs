using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group1FinalProject.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Group1FinalProject.Controllers
{
    public class MAPController : Controller
    {
        // GET: MAP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Redirect()
        {
            return RedirectToAction("ReportView", "Report");
        }

        //this action sends all data in the program to the db
        public ActionResult SendData(finaltable coordinates)
        {
            string reported = "";

            //assigning values to the new object being passed in
            coordinates.flagged = "Y";
            coordinates.username = User.Identity.GetUserId();
            coordinates.datereported = DateTime.Now;
            coordinates.reportedtimes = 1;

            //opening the db
            SquatDBEntities db = new SquatDBEntities();
            
            //adding the object to the DB
            db.finaltables.Add(coordinates);

            try
            {
                //saving the db
                db.SaveChanges();
                
            }
            catch
            {
                //same primary key will prevent the new object from being saved, this will alert the user letting them know
                reported = "Already Reported!";
            }

            //sending the message using temp data
            TempData["reported"] = reported;

            //return View();
            return RedirectToAction("Redirect", "Map");
        }

 
    }
}