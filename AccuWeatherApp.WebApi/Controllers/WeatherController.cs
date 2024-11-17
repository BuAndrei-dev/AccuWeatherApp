using AccuWeatherApp.Data.Models.Weather;
using AccuWeatherApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AccuWeatherApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        ///     Searches for the current weather and rain forecast for a given location Key
        /// </summary>
        /// <param name="locationKey">The City Location Key given by Accu Weather</param>
        /// <returns>A Weather Forecast object containing temperatures and rain forecast for the current day</returns>
        [HttpGet("forecast/{locationKey}")]
        [SwaggerOperation(Summary = "Search for the current weather and rain forecast",
            Description =
                "Returns a Weather Forecast object containing temperatures (Metric and Imperial) and rain forecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeatherForecast>> GetForecast(string locationKey)
        {
            var forecast = await _weatherService.GetCurrentWeatherAsync(locationKey);
            if (forecast == null) return NotFound("Weather data not found");
            return Ok(forecast);
        }
    }
}