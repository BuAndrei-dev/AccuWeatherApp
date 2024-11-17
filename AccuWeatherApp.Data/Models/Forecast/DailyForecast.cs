using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Forecast
{
    public class DailyForecast
    {
        [JsonProperty(nameof(Day))] public DayForecast? Day { get; set; }
    }
}