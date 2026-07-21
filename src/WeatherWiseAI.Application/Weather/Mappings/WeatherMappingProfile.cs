namespace WeatherWiseAI.Application.Weather.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.DTOs;

public class WeatherMappingProfile : Profile
{
    public WeatherMappingProfile()
    {
        CreateMap<WeatherResponseDto, CurrentWeatherResponse>()
            .ForMember(dest => dest.Condition, opt => opt.MapFrom<WeatherWiseAI.Application.Weather.Shared.Mappings.CurrentWeatherCodeResolver>());
    }
}
