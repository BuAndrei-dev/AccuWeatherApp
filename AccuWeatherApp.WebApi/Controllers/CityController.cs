using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AccuWeatherApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        ///     Search Accu Weather API for Cities by Name
        /// </summary>
        /// <param name="cityName">The city name to search for</param>
        /// <returns>A list of cities matching the name</returns>
        [HttpGet("search/{cityName}")]
        [SwaggerOperation(Summary = "Search cities by name",
            Description = "Returns a list of cities matching the provided name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CityResult>>> SearchCities(string cityName)
        {
            var cities = await _cityService.SearchCitiesByNameAsync(cityName);
            if (cities == null || !cities.Any()) return NotFound("No cities found");

            return Ok(cities);
        }
    }
}