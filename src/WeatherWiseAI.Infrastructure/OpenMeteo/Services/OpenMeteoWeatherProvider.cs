namespace WeatherWiseAI.Infrastructure.OpenMeteo.Services;

using AutoMapper;
using Microsoft.Extensions.Logging;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

public class OpenMeteoWeatherProvider : IWeatherProvider
{
    private readonly IOpenMeteoClient _client;
    private readonly IMapper _mapper;
    private readonly ILogger<OpenMeteoWeatherProvider> _logger;

    public OpenMeteoWeatherProvider(
        IOpenMeteoClient client,
        IMapper mapper,
        ILogger<OpenMeteoWeatherProvider> logger)
    {
        _client = client;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<WeatherResponseDto> GetCurrentWeatherAsync(double latitude, double longitude, string units, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching weather data for Lat: {Latitude}, Lon: {Longitude}, Units: {Units}", latitude, longitude, units);
        
        string? tempUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "fahrenheit" : null;
        string? windUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "mph" : null;

        var response = await _client.GetForecastAsync(latitude, longitude, true, tempUnit, windUnit, timezone, null, null, null, cancellationToken);
        
        _logger.LogInformation("Raw Open-Meteo Temp: {Temp}, WeatherCode: {Code}", 
            response?.CurrentWeather?.Temperature, 
            response?.CurrentWeather?.WeatherCode);

        return _mapper.Map<WeatherResponseDto>(response);
    }

    public async Task<WeeklyWeatherDto> GetWeeklyForecastAsync(double latitude, double longitude, string units, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching weekly forecast for Lat: {Latitude}, Lon: {Longitude}, Units: {Units}", latitude, longitude, units);
        
        string? tempUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "fahrenheit" : null;
        string? windUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "mph" : null;
        string dailyParams = "temperature_2m_max,temperature_2m_min,weathercode,windspeed_10m_max";

        var response = await _client.GetForecastAsync(latitude, longitude, false, tempUnit, windUnit, timezone, dailyParams, null, null, cancellationToken);
        
        return _mapper.Map<WeeklyWeatherDto>(response);
    }

    public async Task<ProviderHourlyWeatherDto> GetHourlyForecastAsync(double latitude, double longitude, int hours, string units, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching hourly forecast for Lat: {Latitude}, Lon: {Longitude}, Hours: {Hours}, Units: {Units}", latitude, longitude, hours, units);
        
        string? tempUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "fahrenheit" : null;
        string? windUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "mph" : null;
        string hourlyParams = "temperature_2m,weathercode,windspeed_10m";

        var response = await _client.GetForecastAsync(latitude, longitude, false, tempUnit, windUnit, timezone, null, hourlyParams, hours, cancellationToken);
        
        return _mapper.Map<ProviderHourlyWeatherDto>(response);
    }
}
