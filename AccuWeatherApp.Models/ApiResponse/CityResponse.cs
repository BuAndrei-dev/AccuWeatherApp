using Newtonsoft.Json;

namespace AccuWeatherApp.Models.ApiResponses
{
    public class CityApiResponse
    {
        [JsonProperty("Key")] public string? Key { get; set; }

        [JsonProperty("LocalizedName")] public string? LocalizedName { get; set; }

        [JsonProperty("Country")] public CountryResponse? Country { get; set; }

        [JsonProperty("AdministrativeArea")] public AdministrativeAreaResponse? AdministrativeArea { get; set; }
    }

    public class CountryResponse
    {
        [JsonProperty("ID")] 
        public string? Id { get; set; }

        [JsonProperty("LocalizedName")] 
        public string? LocalizedName { get; set; }
    }

    public class AdministrativeAreaResponse
    {
        [JsonProperty("ID")] 
        public string? Id { get; set; }
        
        [JsonProperty("LocalizedName")] 
        public string? LocalizedName { get; set; }
    }
}