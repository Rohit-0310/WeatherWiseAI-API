namespace WeatherWiseAI.Application.Weather.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.DTOs;

public class WeatherMappingProfile : Profile
{
    public WeatherMappingProfile()
    {
        CreateMap<WeatherResponseDto, CurrentWeatherResponse>()
            .ForMember(dest => dest.Condition, opt => opt.MapFrom<WeatherWiseAI.Application.Weather.Shared.Mappings.CurrentWeatherCodeResolver>())
            .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.ApparentTemperature))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.RelativeHumidity))
            .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.SurfacePressure))
            .ForMember(dest => dest.Visibility, opt => opt.MapFrom(src => src.Visibility))
            .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.WindDirection))
            .ForMember(dest => dest.IsDay, opt => opt.MapFrom(src => src.IsDay))
            .ForMember(dest => dest.MaxTemperature, opt => opt.MapFrom(src => src.MaxTemperature))
            .ForMember(dest => dest.MinTemperature, opt => opt.MapFrom(src => src.MinTemperature))
            .ForMember(dest => dest.UvIndex, opt => opt.MapFrom(src => src.UvIndex))
            .ForMember(dest => dest.PrecipitationChance, opt => opt.MapFrom(src => src.PrecipitationProbability))
            .ForMember(dest => dest.Aqi, opt => opt.MapFrom(src => src.Aqi));
    }
}
