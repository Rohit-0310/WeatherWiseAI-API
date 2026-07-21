namespace WeatherWiseAI.Application.Weather.Forecast.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.Forecast.Responses;
using WeatherWiseAI.Application.Weather.Shared.Mappings;

public class ForecastMappingProfile : Profile
{
    public ForecastMappingProfile()
    {
        CreateMap<WeeklyWeatherDto, ForecastResponseDto>()
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.DailyForecasts))
            .ForMember(dest => dest.Units, opt => opt.Ignore());

        CreateMap<DailyWeatherDto, ForecastDayDto>()
            .ForMember(dest => dest.Condition, opt => opt.MapFrom<DailyWeatherCodeResolver>());
    }
}
