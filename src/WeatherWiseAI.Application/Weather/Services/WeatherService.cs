namespace WeatherWiseAI.Application.Weather.Services;

using AutoMapper;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Application.Weather.DTOs;

public class WeatherService : IWeatherService
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly IMapper _mapper;

    public WeatherService(IWeatherProvider weatherProvider, IMapper mapper)
    {
        _weatherProvider = weatherProvider;
        _mapper = mapper;
    }

    public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(CurrentWeatherRequest request, CancellationToken cancellationToken = default)
    {
        var weatherDto = await _weatherProvider.GetCurrentWeatherAsync(
            request.Latitude, 
            request.Longitude, 
            request.Units.ToString(), 
            request.Timezone, 
            cancellationToken);

        var response = _mapper.Map<CurrentWeatherResponse>(weatherDto);
        response.Units = request.Units.ToString();

        return response;
    }
}
