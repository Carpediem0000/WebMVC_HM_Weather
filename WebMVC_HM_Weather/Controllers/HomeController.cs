using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMVC_HM_Weather.Models;

namespace WebMVC_HM_Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiKey = "bcb395514086595b809ab3007e0d819a"; 

        public async Task<ActionResult> Index(string city = "Kiev")
        {
            if (string.IsNullOrEmpty(city))
            {
                city = "Kiev";
            }

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var weatherInfo = JsonConvert.DeserializeObject<WeatherResponse>(response);

                ViewBag.CityName = city;
                ViewBag.Temperature = weatherInfo.main.temp;
                ViewBag.Description = weatherInfo.weather[0].description;
                ViewBag.Humidity = weatherInfo.main.humidity;
                ViewBag.WindSpeed = weatherInfo.wind.speed;
            }

            return View();
        }
    }
}