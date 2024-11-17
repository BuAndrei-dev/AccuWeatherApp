using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Forecast
{
    /// <summary>
    ///     Gets the forecasts for today (Night and Day)
    /// </summary>
    public class DailyForecastResponse
    {
        [JsonProperty(nameof(DailyForecasts))] public List<DailyForecast>? DailyForecasts { get; set; }
    }
}