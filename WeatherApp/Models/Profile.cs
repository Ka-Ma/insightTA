using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;

namespace WeatherApp.Models
{
    public class Profile
    {
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public string ProfileID { get; set; } //from AspNetUser

        [Display(Name ="Location Key")]
        public string LocationKey { get; set; }
    }
}