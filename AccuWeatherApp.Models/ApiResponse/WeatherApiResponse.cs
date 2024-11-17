using Newtonsoft.Json;

namespace AccuWeatherApp.Models.ApiResponses
{
    public class WeatherApiResponse
    {
        [JsonProperty("HasPrecipitation")] 
        public bool HasPrecipitation { get; set; }

        [JsonProperty("Temperature")] 
        public TemperatureResponse Temperature { get; set; }
    }

    public class TemperatureResponse
    {
        [JsonProperty("Metric")] 
        public TemperatureUnitResponse? Metric { get; set; }

        [JsonProperty("Imperial")] 
        public TemperatureUnitResponse? Imperial { get; set; }
    }

    public class TemperatureUnitResponse
    {
        [JsonProperty("Value")] 
        public double Value { get; set; }

        [JsonProperty("Unit")] 
        public string? Unit { get; set; }
    }
}