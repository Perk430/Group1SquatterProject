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
        
        //this action is where we pull data from the API
        public ActionResult GetONBData(EntryData Input)
        {
            //creates a new request and allows us to send data in the url
            HttpWebRequest request = WebRequest.CreateHttp($"https://search.onboard-apis.com/propertyapi/v1.0.0/property/detail?address1={Input.Address1}&address2={Input.Address2}");

            request.Accept = "application/xml";
            //required headers the API requires before it will send data back
            request.Headers.Add("APIKey", "0ad6a63ba8d89e1de14de3893e417889");
            string oneLine = "";
            string absenteeind = "";
            string propclass = "";
            string propsubtype = "";
            string house = "";
            string wallType = "";
            string garagetype = "";
            
            try
            {
                //gathering response from the API
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //allows us to read the response and convert it to a string
                StreamReader rd = new StreamReader(response.GetResponseStream());

                string data = rd.ReadToEnd();
                //creates a new xml document for which we can search on and call methods
                XmlDocument XMLdataTest = new XmlDocument();
                //loading the string into the xml doc
                XMLdataTest.LoadXml(data);

                try
                {
                    //we are grabbing a single node based on a element path and grabbing the inner text value
                    absenteeind = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/summary/absenteeInd").InnerText;
                }
                catch
                {
                    //return a value if no info is found
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

                try
                {
                    wallType = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/building/construction/wallType").InnerText;
                }
                catch
                {
                    wallType = "No Info";

                }

                try
                {
                    garagetype = XMLdataTest.DocumentElement.SelectSingleNode("/Response/property/building/parking/garagetype").InnerText;
                }
                catch
                {
                    garagetype = "No Info";
                }
            }

            catch
            {
                house = "Not a house!";
            }
            //assigning the results to TempData so we can pass to the report controller
            TempData["oneLine"] = oneLine;
            TempData["absenteeind"] = absenteeind;
            TempData["propclass"] = propclass;
            TempData["propsubtype"] = propsubtype;
            TempData["house"] = house;
            TempData["wallType"] = wallType;
            TempData["garagetype"] = garagetype;

            
            return RedirectToAction("ReportView", "Report");
        }
    }
}