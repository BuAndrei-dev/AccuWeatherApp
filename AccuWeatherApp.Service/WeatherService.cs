using AccuWeatherApp.Models.ApiResponse;
using AccuWeatherApp.Models.ApiResponses;
using AccuWeatherApp.Models.DTO;
using AccuWeatherApp.Service.Configuration;
using AccuWeatherApp.Service.Interface;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AccuWeatherApp.Service
{
    public class WeatherService(
        IOptions<WeatherApiConfiguration> weatherApiConfiguration,
        HttpClient httpClient,
        IMapper mapper)
        : IWeatherService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly WeatherApiConfiguration _weatherApiConfiguration = weatherApiConfiguration.Value;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc />
        public async Task<WeatherDto?> GetCurrentWeatherAsync(string cityKey)
        {
            try
            {
                var currentConditionsUrl =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.CurrentConditionsEndpoint}/{cityKey}?apikey={_weatherApiConfiguration.ApiKey}";
                var currentResponse = await _httpClient.GetAsync(currentConditionsUrl);
                if (!currentResponse.IsSuccessStatusCode)
                {
                    return null;
                }

                var currentJson = await currentResponse.Content.ReadAsStringAsync();
                var currentConditions = JsonConvert.DeserializeObject<List<WeatherApiResponse>>(currentJson)
                    ?.FirstOrDefault();
                var weatherDto = _mapper.Map<WeatherDto>(currentConditions);

                var dailyForecastUrl =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.DailyForecastEndpoint}/{cityKey}?apikey={_weatherApiConfiguration.ApiKey}";
                var dailyResponse = await _httpClient.GetAsync(dailyForecastUrl);

                if (!dailyResponse.IsSuccessStatusCode)
                {
                    return null;
                }

                var dailyJson = await dailyResponse.Content.ReadAsStringAsync();
                var dailyForecast = JsonConvert.DeserializeObject<PrecipitationApiResponse>(dailyJson);

                weatherDto.IsRaining =
                    dailyForecast?.DailyForecasts?.FirstOrDefault()?.Day?.HasPrecipitation ?? false;

                return weatherDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}