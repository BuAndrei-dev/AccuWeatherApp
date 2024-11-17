using AccuWeatherApp.Data.Models.Weather;

namespace AccuWeatherApp.Service.Interface
{
    public interface IWeatherService
    {
        /// <summary>
        ///     Request the current Weather Forecast for a City
        /// </summary>
        /// <param name="cityKey">Unique Location key from Accu Weather</param>
        /// <returns>A Weather Forecast object containing multiple data about the weather forecast</returns>
        Task<WeatherForecast?> GetCurrentWeatherAsync(string cityKey);
    }
}