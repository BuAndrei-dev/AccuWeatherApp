using AccuWeatherApp.Models.DTO;

namespace AccuWeatherApp.Service.Interface
{
    public interface IWeatherService
    {
        /// <summary>
        ///     Request the current Weather Forecast for a City
        /// </summary>
        /// <param name="cityKey">Unique Location key from Accu Weather</param>
        /// <returns>A Weather Forecast object containing multiple data about the weather forecast</returns>
        Task<WeatherDto?> GetCurrentWeatherAsync(string cityKey);
    }
}