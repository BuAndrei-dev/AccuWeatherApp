using AccuWeatherApp.Web.Configuration;
using AccuWeatherApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
                var cityList = JsonConvert.DeserializeObject<List<CityViewModel>>(jsonResponse);

                if (cityList == null || !cityList.Any())
                {
                    ViewBag.Error = "No cities found.";
                    return View("Index");
                }

                return View("Cities", cityList);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occured while searching for cities. {ex.Message}";
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

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject<WeatherViewModel>(jsonResponse);

                if (weather == null)
                {
                    ViewBag.Error = "Weather data not found.";
                    return View("Index");
                }

                ViewBag.CityName = cityName;
                ViewBag.AdministrativeArea = administrativeAreaName;
                return View("WeatherDetails", weather);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occured while retrieving weather data, {ex.Message}";
                return View("Index");
            }
        }
    }
}