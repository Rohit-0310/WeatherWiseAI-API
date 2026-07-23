namespace WeatherWiseAI.Application.Weather.Shared.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.DTOs;
using WeatherWiseAI.Application.Weather.Shared.DTOs;
using WeatherWiseAI.Application.Weather.Shared.Services;

public class CurrentWeatherCodeResolver : IValueResolver<WeatherResponseDto, CurrentWeatherResponse, WeatherConditionDto>
{
    private readonly IWeatherConditionMapper _mapper;

    public CurrentWeatherCodeResolver(IWeatherConditionMapper mapper)
    {
        _mapper = mapper;
    }

    public WeatherConditionDto Resolve(WeatherResponseDto source, CurrentWeatherResponse destination, WeatherConditionDto destMember, ResolutionContext context)
    {
        var normalized = _mapper.MapToNormalizedCondition(source.WeatherCode);
        return new WeatherConditionDto { Code = normalized.Name, Description = normalized.Description, ProviderCode = normalized.ProviderCode };
    }
}
