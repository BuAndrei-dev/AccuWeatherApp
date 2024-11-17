using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.Weather
{
    public class TemperatureUnit
    {
        [JsonProperty(nameof(Value))] public double Value { get; set; }

        [JsonProperty(nameof(Unit))] public string? Unit { get; set; }
    }
}