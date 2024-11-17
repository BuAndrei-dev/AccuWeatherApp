using AccuWeatherApp.Data.Models.City;

namespace AccuWeatherApp.Service.Interface
{
    /// <summary>
    ///     City service
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        ///     Provides a IEnumerable of cities resulted from searching the Accu Weather APP by City Name
        /// </summary>
        /// <param name="cityName">The name of the city we are searching for</param>
        /// <returns>IEnumerable with City Result objects containing multiple data about Cities </returns>
        Task<IEnumerable<CityResult>?> SearchCitiesByNameAsync(string cityName);
    }
}