using AccuWeatherApp.Web.Configuration;
using AccuWeatherApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace AccuWeatherApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiConfiguration _apiConfiguration;
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient client, IOptions<ApiConfiguration> apiConfiguration)
        {
            _httpClient = client;
            _apiConfiguration = apiConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchCity(string cityName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiConfiguration.WebApiUrl}/City/search/{cityName}");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Search for cities failed";
                    return View("Index");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var jsonArray = JArray.Parse(jsonResponse);
                var cities = new List<CityViewModel>();

                foreach (var item in jsonArray)
                {
                    var cityViewModel = new CityViewModel
                    {
                        Id = item["key"]?.ToString(),
                        Name = item["cityName"]?.ToString(),
                        Country = item["country"]?["countryName"]?.ToString(),
                        Region = item["country"]?["id"]?.ToString(),
                        AdministrativeAreaName = item["administrativeArea"]?["countryName"]?.ToString()
                    };

                    cities.Add(cityViewModel);
                }

                if (cities.Count == 0)
                {
                    ViewBag.Error = "No cities found";
                    return View("Index");
                }

                return View("Cities", cities);
            }
            catch
            {
                ViewBag.Error = "An error occured while searching for cities";
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string locationKey, string cityName, string administrativeAreaName)
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{_apiConfiguration.WebApiUrl}/weather/forecast/{locationKey}");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Weather data retrieval failed.";
                    return View("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(json);

                var temperatureC = jsonObject["temperature"]?["celsius"]?["value"]?.ToObject<double>() ?? 0;
                var temperatureF = jsonObject["temperature"]?["fahrenheit"]?["value"]?.ToObject<double>() ?? 0;
                var isRain = jsonObject["isRain"]?.ToObject<bool>() ?? false;

                var weather = new WeatherViewModel
                {
                    Id = locationKey,
                    TemperatureC = temperatureC,
                    TemperatureF = temperatureF,
                    IsRain = isRain
                };
                ViewBag.CityName = cityName;
                ViewBag.AdministrativeArea = administrativeAreaName;
                return View("WeatherDetails", weather);
            }
            catch
            {
                ViewBag.Error = "An error occured while retrieving weather data";
                return View("Index");
            }
        }
    }
}