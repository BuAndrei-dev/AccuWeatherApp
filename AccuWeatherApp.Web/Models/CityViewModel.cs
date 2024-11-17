using Newtonsoft.Json;

namespace AccuWeatherApp.Web.Models
{
    public class CityViewModel
    {
        [JsonProperty("key")]
        public string? Id { get; set; }

        [JsonProperty("localizedName")]
        public string? Name { get; set; }

        [JsonProperty("countryName")]
        public string? Country { get; set; }

        [JsonProperty("regionName")]
        public string? Region { get; set; }

        [JsonProperty("administrativeAreaName")]
        public string? AdministrativeAreaName { get; set; }
    }
}