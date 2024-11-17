using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Weather
{
    /// <summary>
    ///     Model used to get the Weather Response from Accu Weather API
    /// </summary>
    public class WeatherForecast
    {
        public int Id { get; set; }

        [JsonProperty("Temperature")] public Temperature? Temperature { get; set; }

        [JsonProperty("IsRain")] public bool IsRain { get; set; }
    }
}