using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using Microsoft.AspNet.Identity;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //if user is not logged in redirect to login/register otherwise just return view
            var loggedIn = System.Web.HttpContext.Current.User?.Identity.IsAuthenticated ?? false;
            var currentUserId = User?.Identity.GetUserId();
            var profile = db.Profiles.ToList().Where(w => w.ProfileID == currentUserId);

            if (!loggedIn)
            {
                return RedirectToAction("Login", "Account");
            
            }else if (loggedIn && !(profile.Count() > 0))
            {
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
        
        

        public JsonResult GetRegions()
        {
            Weather weather = new Weather();
            return Json(weather.getRegions(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountryByRegion(string region)
        {
            Weather weather = new Weather();
            return Json(weather.getCountryByRegion(region), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAdminAreaByCountry(string country)
        {
            Weather weather = new Weather();
            return Json(weather.getAdminAreaByCountry(country), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityByCountryAndAdmin(string country, string admin, string search)
        {
            Weather weather = new Weather();
            return Json(weather.getCityByCountryAndAdmin(country, admin, search), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentProfile()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.ToList().Where(w => w.ProfileID == currentUserId);

            return Json(currentProfile, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentProfileName()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.ToList().Where(w => w.ProfileID == currentUserId);
            string currentName = "";

            foreach (var p in currentProfile)
            {
                currentName = p.LocationName;
            }

            return Json(currentName, JsonRequestBehavior.AllowGet);
        }

        public string GetCurrentProfileKey()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.ToList().Where(w => w.ProfileID == currentUserId);
            string currentKey = "";
            
            foreach(var p in currentProfile)
            {
                currentKey = p.LocationKey;
            }

            return currentKey;
        }

        public JsonResult GetCurrentProfileWeather()
        {
            return GetForecastByKey(GetCurrentProfileKey());
        }

        public JsonResult GetForecastByKey(string key)
        {
            Weather weather = new Weather();
            JsonResult result = Json(weather.getForecastByKey(key), JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}