using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class ProfileViewModel
    {
        public string ProfileID { get; set; } //from AspNetUser

        [Display(Name = "Location Key")]
        public string LocationKey { get; set; }

        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        public bool exists { get; set; }

    }
}