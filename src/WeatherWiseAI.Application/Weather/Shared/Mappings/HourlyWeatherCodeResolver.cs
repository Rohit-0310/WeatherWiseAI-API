namespace WeatherWiseAI.Application.Weather.Shared.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.Hourly.Responses;
using WeatherWiseAI.Application.Weather.Shared.DTOs;
using WeatherWiseAI.Application.Weather.Shared.Services;

public class HourlyWeatherCodeResolver : IValueResolver<ProviderHourlyForecastDto, HourlyForecastDto, WeatherConditionDto>
{
    private readonly IWeatherConditionMapper _mapper;

    public HourlyWeatherCodeResolver(IWeatherConditionMapper mapper)
    {
        _mapper = mapper;
    }

    public WeatherConditionDto Resolve(ProviderHourlyForecastDto source, HourlyForecastDto destination, WeatherConditionDto destMember, ResolutionContext context)
    {
        var normalized = _mapper.MapToNormalizedCondition(source.WeatherCode);
        return new WeatherConditionDto { Code = normalized.Name, Description = normalized.Description };
    }
}
