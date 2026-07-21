namespace WeatherWiseAI.Application.Weather.Hourly.Services;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Application.Weather.Hourly.Interfaces;
using WeatherWiseAI.Application.Weather.Hourly.Requests;
using WeatherWiseAI.Application.Weather.Hourly.Responses;

public class HourlyService : IHourlyService
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly IMapper _mapper;

    public HourlyService(IWeatherProvider weatherProvider, IMapper mapper)
    {
        _weatherProvider = weatherProvider;
        _mapper = mapper;
    }

    public async Task<HourlyResponseDto> GetHourlyForecastAsync(HourlyRequest request, CancellationToken cancellationToken = default)
    {
        var providerDto = await _weatherProvider.GetHourlyForecastAsync(
            request.Latitude, 
            request.Longitude, 
            request.Hours,
            request.Units.ToString(), 
            request.Timezone, 
            cancellationToken);

        var response = _mapper.Map<HourlyResponseDto>(providerDto);
        response.Units = request.Units.ToString();

        return response;
    }
}
