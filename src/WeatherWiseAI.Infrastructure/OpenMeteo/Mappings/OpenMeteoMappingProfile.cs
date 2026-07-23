namespace WeatherWiseAI.Infrastructure.OpenMeteo.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

public class OpenMeteoMappingProfile : Profile
{
    public OpenMeteoMappingProfile()
    {
        CreateMap<OpenMeteoResponse, WeatherResponseDto>()
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Current != null ? src.Current.Temperature : (src.CurrentWeather != null ? src.CurrentWeather.Temperature : 0)))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Current != null ? src.Current.WindSpeed : (src.CurrentWeather != null ? src.CurrentWeather.Windspeed : 0)))
            .ForMember(dest => dest.WeatherCode, opt => opt.MapFrom(src => src.Current != null ? src.Current.WeatherCode : (src.CurrentWeather != null ? src.CurrentWeather.WeatherCode : 0)))
            .ForMember(dest => dest.ApparentTemperature, opt => opt.MapFrom(src => src.Current != null ? src.Current.ApparentTemperature : 0))
            .ForMember(dest => dest.RelativeHumidity, opt => opt.MapFrom(src => src.Current != null ? src.Current.RelativeHumidity : 0))
            .ForMember(dest => dest.SurfacePressure, opt => opt.MapFrom(src => src.Current != null ? src.Current.SurfacePressure : 0))
            .ForMember(dest => dest.Visibility, opt => opt.MapFrom(src => src.Current != null ? src.Current.Visibility : 0))
            .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.Current != null ? src.Current.WindDirection : 0))
            .ForMember(dest => dest.IsDay, opt => opt.MapFrom(src => src.Current != null ? src.Current.IsDay : 0))
            .ForMember(dest => dest.MaxTemperature, opt => opt.MapFrom(src => src.Daily != null && src.Daily.TemperatureMax.Count > 0 ? src.Daily.TemperatureMax[0] : 0))
            .ForMember(dest => dest.MinTemperature, opt => opt.MapFrom(src => src.Daily != null && src.Daily.TemperatureMin.Count > 0 ? src.Daily.TemperatureMin[0] : 0))
            .ForMember(dest => dest.UvIndex, opt => opt.MapFrom(src => src.Daily != null && src.Daily.UvIndexMax.Count > 0 ? src.Daily.UvIndexMax[0] : 0))
            .ForMember(dest => dest.PrecipitationProbability, opt => opt.MapFrom(src => src.Daily != null && src.Daily.PrecipitationProbabilityMax.Count > 0 ? src.Daily.PrecipitationProbabilityMax[0] : 0));

        CreateMap<OpenMeteoResponse, WeeklyWeatherDto>()
            .ForMember(dest => dest.DailyForecasts, opt => opt.MapFrom(src => MapDailyForecasts(src.Daily)));

        CreateMap<OpenMeteoResponse, ProviderHourlyWeatherDto>()
            .ForMember(dest => dest.HourlyForecasts, opt => opt.MapFrom(src => MapHourlyForecasts(src.Hourly)));
    }

    private static System.Collections.Generic.List<ProviderHourlyForecastDto> MapHourlyForecasts(OpenMeteoHourlyDto? hourly)
    {
        var list = new System.Collections.Generic.List<ProviderHourlyForecastDto>();
        if (hourly == null || hourly.Time == null) return list;

        for (int i = 0; i < hourly.Time.Count; i++)
        {
            list.Add(new ProviderHourlyForecastDto
            {
                Time = System.DateTime.TryParse(hourly.Time[i], out var date) ? date : System.DateTime.MinValue,
                Temperature = hourly.Temperature.Count > i ? hourly.Temperature[i] : 0,
                WindSpeed = hourly.WindSpeed.Count > i ? hourly.WindSpeed[i] : 0,
                WeatherCode = hourly.WeatherCode.Count > i ? hourly.WeatherCode[i] : 0,
                PrecipitationChance = hourly.PrecipitationProbability.Count > i ? hourly.PrecipitationProbability[i] : 0
            });
        }
        return list;
    }

    private static System.Collections.Generic.List<DailyWeatherDto> MapDailyForecasts(OpenMeteoDailyDto? daily)
    {
        var list = new System.Collections.Generic.List<DailyWeatherDto>();
        if (daily == null || daily.Time == null) return list;

        for (int i = 0; i < daily.Time.Count; i++)
        {
            list.Add(new DailyWeatherDto
            {
                Date = System.DateTime.TryParse(daily.Time[i], out var date) ? date : System.DateTime.MinValue,
                WeatherCode = daily.WeatherCode.Count > i ? daily.WeatherCode[i] : 0,
                MaxTemperature = daily.TemperatureMax.Count > i ? daily.TemperatureMax[i] : 0,
                MinTemperature = daily.TemperatureMin.Count > i ? daily.TemperatureMin[i] : 0,
                MaxWindSpeed = daily.WindSpeedMax.Count > i ? daily.WindSpeedMax[i] : 0,
                PrecipitationChance = daily.PrecipitationProbabilityMax.Count > i ? daily.PrecipitationProbabilityMax[i] : 0
            });
        }
        return list;
    }
}
