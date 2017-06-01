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
        public ActionResult SendData(finaltable coordinates)
        {
            string reported = "";

            coordinates.flagged = "y";
            coordinates.username = User.Identity.GetUserId();
            coordinates.datereported = DateTime.Now;
            coordinates.reportedtimes = 1;

            SquatDBEntities db = new SquatDBEntities();
            db.finaltables.Add(coordinates);

            try
            {
                db.SaveChanges();
                
            }
            catch
            {
                reported = "Already Reported!";
            }

            TempData["reported"] = reported;

            //return View();
            return RedirectToAction("Redirect", "Map");
        }

 
    }
}