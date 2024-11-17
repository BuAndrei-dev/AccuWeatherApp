using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Data.Models.Weather;
using AccuWeatherApp.Models.ApiResponse;
using AccuWeatherApp.Models.ApiResponses;
using AccuWeatherApp.Models.DTO;
using AccuWeatherApp.Models.DTOs;
using AccuWeatherApp.Web.Models;
using AutoMapper;

namespace AccuWeatherApp.Service.Configuration
{
    public class AccuAutoMapper : Profile
    {
        public AccuAutoMapper()
        {
            CreateMap<CityApiResponse, CityDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.LocalizedName))
                .ForMember(dest => dest.AdministrativeAreaName,
                    opt => opt.MapFrom(src => src.AdministrativeArea.LocalizedName))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Country.Id));

            CreateMap<CityDto, City>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.AdministrativeAreaName, opt => opt.MapFrom(src => src.AdministrativeAreaName))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.RegionName));

            CreateMap<CityDto, CityViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LocalizedName))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.AdministrativeAreaName, opt => opt.MapFrom(src => src.AdministrativeAreaName))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.RegionName));

            CreateMap<CityApiResponse, City>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.LocalizedName, opt => opt.MapFrom(src => src.LocalizedName))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.LocalizedName))
                .ForMember(dest => dest.AdministrativeAreaName,
                    opt => opt.MapFrom(src => src.AdministrativeArea.LocalizedName))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Country.Id));

            CreateMap<WeatherApiResponse, WeatherDto>()
                .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.Temperature.Metric.Value))
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.Temperature.Imperial.Value))
                .ForMember(dest => dest.IsRaining, opt => opt.MapFrom(src => src.HasPrecipitation));

            CreateMap<WeatherDto, Weather>()
                .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.TemperatureC))
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TemperatureF))
                .ForMember(dest => dest.IsRain, opt => opt.MapFrom(src => src.IsRaining));

            CreateMap<Weather, WeatherDto>()
                .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.TemperatureC))
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TemperatureF))
                .ForMember(dest => dest.IsRaining, opt => opt.MapFrom(src => src.IsRain));

            CreateMap<WeatherDto, WeatherViewModel>()
                .ForMember(dest => dest.IsRain, opt => opt.MapFrom(src => src.IsRaining))
                .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.TemperatureC))
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TemperatureF));

            CreateMap<PrecipitationApiResponse, PrecipitationDto>()
                .ForMember(dest => dest.IsRainExpected,
                    opt => opt.MapFrom(src => src.DailyForecasts.FirstOrDefault().Day.HasPrecipitation));

            CreateMap<PrecipitationDto, Weather>()
                .ForMember(dest => dest.IsRain, opt => opt.MapFrom(src => src.IsRainExpected))
                ;

            // Map PrecipitationDto to WeatherViewModel (UI Model)
            CreateMap<PrecipitationDto, WeatherViewModel>()
                .ForMember(dest => dest.IsRain, opt => opt.MapFrom(src => src.IsRainExpected));
        }
    }
}