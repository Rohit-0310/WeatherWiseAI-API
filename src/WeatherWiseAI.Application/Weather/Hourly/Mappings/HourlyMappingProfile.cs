namespace WeatherWiseAI.Application.Weather.Hourly.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.Hourly.Responses;
using WeatherWiseAI.Application.Weather.Shared.Mappings;

public class HourlyMappingProfile : Profile
{
    public HourlyMappingProfile()
    {
        CreateMap<ProviderHourlyWeatherDto, HourlyResponseDto>()
            .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.HourlyForecasts))
            .ForMember(dest => dest.Units, opt => opt.Ignore());

        CreateMap<ProviderHourlyForecastDto, HourlyForecastDto>()
            .ForMember(dest => dest.Condition, opt => opt.MapFrom<HourlyWeatherCodeResolver>());
    }
}
