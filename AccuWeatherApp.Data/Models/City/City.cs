namespace AccuWeatherApp.Data.Models.City
{
    public class City
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? LocalizedName { get; set; }
        public string? CountryName { get; set; }
        public string? RegionName { get; set; }
        public string? AdministrativeAreaName { get; set; }
    }
}