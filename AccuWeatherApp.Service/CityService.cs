using AccuWeatherApp.Data.Context;
using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Models.ApiResponses;
using AccuWeatherApp.Models.DTO;
using AccuWeatherApp.Service.Configuration;
using AccuWeatherApp.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AccuWeatherApp.Service
{
    public class CityService(
        WeatherDbContext dbContext,
        HttpClient httpClient,
        IOptions<WeatherApiConfiguration> weatherApiConfiguration,
        IMapper mapper)
        : ICityService
    {
        private readonly WeatherApiConfiguration _weatherApiConfiguration = weatherApiConfiguration.Value;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc />
        public async Task<IEnumerable<CityDto>> SearchCitiesByNameAsync(string cityName)
        {
            try
            {
                var cachedCities = (await CheckCachedCities(cityName)).ToList();

                if (cachedCities.Any()) return cachedCities;

                var url =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.LocationEndpoint}/search.json?apikey={_weatherApiConfiguration.ApiKey}&q={cityName}";
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return Enumerable.Empty<CityDto>();

                var json = await response.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<List<CityApiResponse>>(json);

                if (cities == null) return Enumerable.Empty<CityDto>();

                foreach (var cityDto in cities)
                {
                    var cityEntity = _mapper.Map<City>(cityDto);
                    await dbContext.City.AddAsync(cityEntity);
                }

                var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);
                return cityDtos;
            }
            catch (Exception)
            {
                return Enumerable.Empty<CityDto>();
            }
        }

        private async Task<IEnumerable<CityDto>> CheckCachedCities(string cityName)
        {
            var cities = await dbContext.City
                .Where(c => EF.Functions.Like(c.LocalizedName, $"{cityName}%"))
                .ToListAsync();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }
    }
}