using AccuWeatherApp.Data.Context;
using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Service.Configuration;
using AccuWeatherApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AccuWeatherApp.Service
{
    public class CityService(
        WeatherDbContext dbContext,
        HttpClient httpClient,
        IOptions<WeatherApiConfiguration> weatherApiConfiguration)
        : ICityService
    {
        private readonly WeatherApiConfiguration _weatherApiConfiguration = weatherApiConfiguration.Value;

        /// <inheritdoc />
        public async Task<IEnumerable<CityResult>?> SearchCitiesByNameAsync(string cityName)
        {
            try
            {
                var cachedCities = (await CheckCachedCities(cityName)).ToList();

                if (cachedCities.Any()) return cachedCities;

                var url =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.LocationEndpoint}/search.json?apikey={_weatherApiConfiguration.ApiKey}&q={cityName}";
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<List<CityResult>>(json);

                if (cities != null)
                {
                    foreach (var city in cities)
                    {
                        var existingCountry = await dbContext.Country
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => city.Country != null && c.Id == city.Country.Id);

                        if (existingCountry != null) city.Country = existingCountry;

                        if (city.AdministrativeArea != null)
                        {
                            var existingArea = await dbContext.AdministrativeArea
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == city.AdministrativeArea.Id);
                            if (existingArea != null) city.AdministrativeArea = existingArea;
                        }
                    }

                    await dbContext.SaveChangesAsync();

                    return cities;
                }

                return null;
            }
            catch (Exception)
            {
                return Enumerable.Empty<CityResult>();
            }
        }

        private async Task<IEnumerable<CityResult>> CheckCachedCities(string cityName)
        {
            return await dbContext.CityResult
                .Where(c => EF.Functions.Like(c.CityName, $"{cityName}%"))
                .ToListAsync();
        }
    }
}