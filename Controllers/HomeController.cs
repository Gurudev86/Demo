using API_Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace API_Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "API Demo";

            return View();
        }

        public JsonResult Candidate()
        {

            var candidateData = new Candidate
            {
                Name = "Richard",
                Phone = "12456678",
                Address= "Sydney"
            };

            var result = JsonConvert.SerializeObject(candidateData);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult Listings(int nop)
        {
            List <VehicleList > filterByTotalPrice = new List<VehicleList>();
            using (var webClient = new System.Net.WebClient())
            {
                var jsonData = webClient.DownloadString("https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest");

                var serializer = new JavaScriptSerializer();
                Quotes lstQuotes = serializer.Deserialize<Quotes>(jsonData);

                var filterList = lstQuotes.listings.Where(dt => dt.vehicleType.maxPassengers != nop).ToList();

                foreach(var item in filterList)
                {
                    item.totalprice = item.pricePerPassenger * item.vehicleType.maxPassengers;
                }

                filterByTotalPrice = filterList.OrderBy(dt => dt.totalprice).ToList();
            }

            var result = JsonConvert.SerializeObject(filterByTotalPrice);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Location(string ipaddress)
        {
            string[] arrValue = new string[] { };
            using (var webClient = new System.Net.WebClient())
            {
                var jsonData = webClient.DownloadString("http://api.ipstack.com/" + ipaddress + "?access_key=d6ef339995e520ccf55e2ed154f377b6&format=1");

                var filterData = jsonData.Split(new string[] { "\"city\": \"" }, StringSplitOptions.None);
                arrValue = filterData[1].Split(new string[] { "\", \"" }, StringSplitOptions.None);
            }
            
            return Json(arrValue[0], JsonRequestBehavior.AllowGet);
        }
    }
}
