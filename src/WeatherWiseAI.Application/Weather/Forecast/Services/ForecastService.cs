namespace WeatherWiseAI.Application.Weather.Forecast.Services;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Application.Weather.Forecast.Interfaces;
using WeatherWiseAI.Application.Weather.Forecast.Requests;
using WeatherWiseAI.Application.Weather.Forecast.Responses;

public class ForecastService : IForecastService
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly IMapper _mapper;

    public ForecastService(IWeatherProvider weatherProvider, IMapper mapper)
    {
        _weatherProvider = weatherProvider;
        _mapper = mapper;
    }

    public async Task<ForecastResponseDto> GetWeeklyForecastAsync(ForecastRequest request, CancellationToken cancellationToken = default)
    {
        var weeklyDto = await _weatherProvider.GetWeeklyForecastAsync(
            request.Latitude, 
            request.Longitude, 
            request.Units.ToString(), 
            request.Timezone, 
            cancellationToken);

        var response = _mapper.Map<ForecastResponseDto>(weeklyDto);
        response.Units = request.Units.ToString();

        return response;
    }
}
