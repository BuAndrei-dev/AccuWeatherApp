using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Weather
{
    /// <summary>
    ///     Used to represent temperature for both metric and imperial
    /// </summary>
    public class Temperature
    {
        [JsonProperty("Metric")] public TemperatureUnit? Celsius { get; set; }

        [JsonProperty("Imperial")] public TemperatureUnit? Fahrenheit { get; set; }
    }
}