using AccuWeatherApp.Data.Models.Forecast;
using AccuWeatherApp.Data.Models.Weather;
using AccuWeatherApp.Service.Configuration;
using AccuWeatherApp.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AccuWeatherApp.Service
{
    public class WeatherService(IOptions<WeatherApiConfiguration> weatherApiConfiguration, HttpClient httpClient)
        : IWeatherService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly WeatherApiConfiguration _weatherApiConfiguration = weatherApiConfiguration.Value;

        /// <inheritdoc />
        public async Task<WeatherForecast?> GetCurrentWeatherAsync(string cityKey)
        {
            try
            {
                var weatherForecast = new WeatherForecast();

                // Get Current Conditions
                var currentConditionsUrl =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.CurrentConditionsEndpoint}/{cityKey}?apikey={_weatherApiConfiguration.ApiKey}";
                var currentResponse = await _httpClient.GetAsync(currentConditionsUrl);

                if (currentResponse.IsSuccessStatusCode)
                {
                    var currentJson = await currentResponse.Content.ReadAsStringAsync();
                    var currentConditions = JsonConvert.DeserializeObject<List<WeatherForecast>>(currentJson)
                        ?.FirstOrDefault();

                    if (currentConditions != null) weatherForecast.Temperature = currentConditions.Temperature;
                }

                // Get Daily Forecast to check for rain
                var dailyForecastUrl =
                    $"{_weatherApiConfiguration.AccuWeatherBaseUrl}{_weatherApiConfiguration.DailyForecastEndpoint}/{cityKey}?apikey={_weatherApiConfiguration.ApiKey}";
                var dailyResponse = await _httpClient.GetAsync(dailyForecastUrl);

                if (dailyResponse.IsSuccessStatusCode)
                {
                    var dailyJson = await dailyResponse.Content.ReadAsStringAsync();
                    var dailyForecast = JsonConvert.DeserializeObject<DailyForecastResponse>(dailyJson);

                    if (dailyForecast != null)
                        if (dailyForecast.DailyForecasts != null)
                            weatherForecast.IsRain =
                                dailyForecast.DailyForecasts.FirstOrDefault()?.Day.HasPrecipitation ?? false;
                }

                return weatherForecast;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}