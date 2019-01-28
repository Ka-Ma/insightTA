using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class ProfileController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Profile
        public ActionResult Index()
        {

            var currentUserId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.SingleOrDefault(w => w.ProfileID == currentUserId);

            if (currentProfile != null)
            {
                return RedirectToAction("Edit", "Profile", new { @exists = true });
            }
            else
            {
                return RedirectToAction("Edit", "Profile", new { @exists = false });
            }
        }

        public ActionResult Edit(bool exists = false)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.SingleOrDefault(w => w.ProfileID == currentUserId);
            ProfileViewModel vm = new ProfileViewModel();

            vm.ProfileID = currentUserId;
            if (currentProfile != null)
            {
                vm.LocationKey = currentProfile.LocationKey;
                vm.LocationName = currentProfile.LocationName;
            }
            vm.exists = exists;
                            
            return View(vm);
        }

        public ActionResult AddProfile(string locationKey, string locationName)
        {
            var profile = new Profile();
            profile.ProfileID = User.Identity.GetUserId();
            profile.LocationKey = locationKey;
            profile.LocationName = locationName;

            db.Profiles.Add(profile);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home", null);

        }

        public ActionResult UpdateProfile(string locationKey, string locationName)
        {
            //this is a bit hacky because I obviously need to spend more time learning how to integrate my own tables with the aspnet authentication db
            var currentUserId = User.Identity.GetUserId();
            
            var current = db.Profiles.SingleOrDefault(w => w.ProfileID == currentUserId);
            if(current != null) {
                var update = new Profile();
                update.ProfileID = current.ProfileID;
                update.LocationKey = locationKey;
                update.LocationName = locationName;

                db.Profiles.Remove(current);
                db.Profiles.Add(update);
                                        
                db.SaveChanges();

                return RedirectToAction("Index","Home",null);
            }
            
            return Json("ERROR", JsonRequestBehavior.AllowGet);

        }
    }
}