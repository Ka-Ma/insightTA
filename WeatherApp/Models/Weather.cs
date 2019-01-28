using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WeatherApp.Models
{
    public class Weather
    {
        private string APIKEY = "YrHzVlO4Z0XUt0UJNJYaZXfIPtXUyilD";

        public Object getRegions()
        {
            string url = "http://dataservice.accuweather.com/locations/v1/regions?apikey="+ APIKEY;

            return getJsonUrl(url);
        }

        public Object getCountryByRegion(string region)
        {
            string url = "http://dataservice.accuweather.com/locations/v1/countries/" + region + "?apikey=" + APIKEY;

            return getJsonUrl(url);
        }

        public Object getAdminAreaByCountry(string country)
        {
            string url = "http://dataservice.accuweather.com/locations/v1/adminareas/" + country + "?apikey=" + APIKEY;

            return getJsonUrl(url);
        }

        public Object getCityByRegion(string region, string citySearch)
        {
            string url = "http://dataservice.accuweather.com/locations/v1/cities/"+region+"/search?apikey="+APIKEY+"&q="+citySearch;

            return getJsonUrl(url);
        }

        public Object getCityByCountryAndAdmin(string country, string admin, string citySearch)
        {
            string url = "http://dataservice.accuweather.com/locations/v1/cities/" + country + "/" + admin + "/search?apikey=" + APIKEY + "&q=" + citySearch;

            return getJsonUrl(url);
        }

        public Object getForecastByKey(string key) {
            string url = "http://dataservice.accuweather.com/currentconditions/v1/" + key + "?apikey=" + APIKEY;

            return getJsonUrl(url);
            }

        private Object getJsonUrl(string url)
        {
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
    }
}