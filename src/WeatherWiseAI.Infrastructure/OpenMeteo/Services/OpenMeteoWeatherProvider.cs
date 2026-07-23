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

        var responseTask = _client.GetForecastAsync(
            latitude: latitude, 
            longitude: longitude, 
            currentWeather: false, 
            current: "temperature_2m,relative_humidity_2m,apparent_temperature,is_day,weather_code,cloud_cover,surface_pressure,wind_speed_10m,wind_direction_10m,visibility",
            temperatureUnit: tempUnit, 
            windSpeedUnit: windUnit, 
            timezone: timezone ?? "auto", 
            daily: "temperature_2m_max,temperature_2m_min,precipitation_probability_max,uv_index_max", 
            cancellationToken: cancellationToken);

        var aqiTask = _client.GetAirQualityAsync(latitude, longitude, "european_aqi,us_aqi", cancellationToken);

        await Task.WhenAll(responseTask, aqiTask);
        
        var response = responseTask.Result;
        var aqiResponse = aqiTask.Result;
        
        _logger.LogInformation("Raw Open-Meteo Temp: {Temp}, WeatherCode: {Code}", 
            response?.Current?.Temperature, 
            response?.Current?.WeatherCode);

        var mapped = _mapper.Map<WeatherResponseDto>(response);
        
        if (aqiResponse?.Current != null)
        {
            mapped.Aqi = aqiResponse.Current.EuropeanAqi;
        }

        return mapped;
    }

    public async Task<WeeklyWeatherDto> GetWeeklyForecastAsync(double latitude, double longitude, string units, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching weekly forecast for Lat: {Latitude}, Lon: {Longitude}, Units: {Units}", latitude, longitude, units);
        
        string? tempUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "fahrenheit" : null;
        string? windUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "mph" : null;
        string dailyParams = "temperature_2m_max,temperature_2m_min,weathercode,windspeed_10m_max,precipitation_probability_max,uv_index_max";

        var response = await _client.GetForecastAsync(
            latitude: latitude,
            longitude: longitude,
            currentWeather: false,
            temperatureUnit: tempUnit,
            windSpeedUnit: windUnit,
            timezone: timezone,
            daily: dailyParams,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<WeeklyWeatherDto>(response);
    }

    public async Task<ProviderHourlyWeatherDto> GetHourlyForecastAsync(double latitude, double longitude, int hours, string units, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching hourly forecast for Lat: {Latitude}, Lon: {Longitude}, Hours: {Hours}, Units: {Units}", latitude, longitude, hours, units);
        
        string? tempUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "fahrenheit" : null;
        string? windUnit = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase) ? "mph" : null;
        string hourlyParams = "temperature_2m,weathercode,windspeed_10m,precipitation_probability";

        var response = await _client.GetForecastAsync(
            latitude: latitude,
            longitude: longitude,
            currentWeather: false,
            temperatureUnit: tempUnit,
            windSpeedUnit: windUnit,
            timezone: timezone,
            hourly: hourlyParams,
            forecastHours: hours,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<ProviderHourlyWeatherDto>(response);
    }

    public async Task<ProviderAstronomyDto> GetAstronomyDataAsync(double latitude, double longitude, DateTime date, string? timezone, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching astronomy data for Lat: {Latitude}, Lon: {Longitude}, Date: {Date}", latitude, longitude, date);
        
        string dailyParams = "sunrise,sunset,daylight_duration,sunshine_duration";
        string dateString = date.ToString("yyyy-MM-dd");

        var response = await _client.GetForecastAsync(
            latitude: latitude, 
            longitude: longitude, 
            currentWeather: false, 
            timezone: timezone ?? "auto", 
            daily: dailyParams, 
            startDate: dateString, 
            endDate: dateString, 
            cancellationToken: cancellationToken);

        if (response?.Daily == null || response.Daily.Time.Count == 0)
        {
            throw new Exception("Failed to retrieve astronomy data from Open-Meteo.");
        }

        return new ProviderAstronomyDto
        {
            Sunrise = DateTime.Parse(response.Daily.Sunrise[0]),
            Sunset = DateTime.Parse(response.Daily.Sunset[0]),
            DaylightDuration = response.Daily.DaylightDuration[0],
            SunshineDuration = response.Daily.SunshineDuration[0]
        };
    }
}
