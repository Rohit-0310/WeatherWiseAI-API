namespace WeatherWiseAI.Application.Weather.Shared.Mappings;

using AutoMapper;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Weather.Forecast.Responses;
using WeatherWiseAI.Application.Weather.Shared.DTOs;
using WeatherWiseAI.Application.Weather.Shared.Services;

public class DailyWeatherCodeResolver : IValueResolver<DailyWeatherDto, ForecastDayDto, WeatherConditionDto>
{
    private readonly IWeatherConditionMapper _mapper;

    public DailyWeatherCodeResolver(IWeatherConditionMapper mapper)
    {
        _mapper = mapper;
    }

    public WeatherConditionDto Resolve(DailyWeatherDto source, ForecastDayDto destination, WeatherConditionDto destMember, ResolutionContext context)
    {
        var normalized = _mapper.MapToNormalizedCondition(source.WeatherCode);
        return new WeatherConditionDto { Code = normalized.Name, Description = normalized.Description };
    }
}
