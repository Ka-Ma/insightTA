using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext db;

        private HomeController(ApplicationDbContext db)
        {
            db = this.db;
        }

        public ActionResult Index()
        {
            //if user is not logged in redirect to login/register otherwise just return view
            var loggedIn = System.Web.HttpContext.Current.User?.Identity.IsAuthenticated ?? false;
            var profile = false;//look up profile for user logged in
            
            if (!loggedIn)
            {
                return RedirectToAction("Login", "Account", new { area = "Profile" });
            
            }else if (loggedIn && !profile)
            {
                return RedirectToAction("Profile", "Home");
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
        
        public ActionResult Profile()
        {
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
    }
}