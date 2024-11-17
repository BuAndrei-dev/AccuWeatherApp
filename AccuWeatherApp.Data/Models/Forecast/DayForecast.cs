using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Forecast
{
    public class DayForecast
    {
        [JsonProperty(nameof(HasPrecipitation))]
        public bool HasPrecipitation { get; set; }
    }
}