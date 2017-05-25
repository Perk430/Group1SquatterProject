using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Group1FinalProject.Models;

namespace Group1FinalProject.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetONBData(EntryData Input)
        {
            //formatting for properties
            //Input.Address1.ToString();
            //Input.Address1.Replace(" ", "%20");

            //Input.Address2.ToString();
            //Input.Address2.Replace(" ", "%20").Replace(",","C");

            //calling API with formatted inputs
            HttpWebRequest request = WebRequest.CreateHttp($"https://search.onboard-apis.com/propertyapi/v1.0.0/property/detail?address1={Input.Address1}&address2={Input.Address2}");

            request.Accept = "application/xml";

            request.Headers.Add("APIKey", "0ad6a63ba8d89e1de14de3893e417889");
            string oneLine = "";
            string absenteeind = "";
            string propclass = "";
            string propsubtype = "";
            //request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader rd = new StreamReader(response.GetResponseStream());

                string data = rd.ReadToEnd();

                XmlDocument XMLdataTest = new XmlDocument();

                XMLdataTest.LoadXml(data);

            try
            {
                absenteeind = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/summary/absenteeInd").InnerText;
            }
            catch
            {
                absenteeind = "No Info";
            }


            try
            {
                oneLine = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/address/oneLine").InnerText;
            }
            catch
            {
                oneLine = "No info";
            }


            try
            {
                propclass = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/summary/propclass").InnerText;
            }
            catch
            {
                propclass = "No Info";
            }

            try
            {
                propsubtype = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/summary/propsubtype").InnerText;
            }
            catch
            {
                propsubtype = "No info";
            }
            }
            catch
            {
                ViewBag.Message = "Not a house!";
            }


            TempData["oneLine"] = oneLine;
            TempData["absenteeind"] = absenteeind;
            TempData["propclass"] = propclass;
            TempData["propsubtype"] = propsubtype;


            // return View("../Home/ReportView");
            return RedirectToAction("ReportView", "Home");
        }
    }
}