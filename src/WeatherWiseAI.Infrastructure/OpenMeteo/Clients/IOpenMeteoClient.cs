namespace WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

using Refit;
using WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

public interface IOpenMeteoClient
{
    [Get("/v1/forecast")]
    Task<OpenMeteoResponse> GetForecastAsync(
        [Query] double latitude, 
        [Query] double longitude, 
        [Query("current_weather")] bool currentWeather = true,
        [Query("temperature_unit")] string? temperatureUnit = null,
        [Query("windspeed_unit")] string? windSpeedUnit = null,
        [Query("timezone")] string? timezone = null,
        [Query("daily")] string? daily = null,
        [Query("hourly")] string? hourly = null,
        [Query("forecast_hours")] int? forecastHours = null,
        CancellationToken cancellationToken = default);

    [Get("https://geocoding-api.open-meteo.com/v1/search")]
    Task<OpenMeteoGeocodingResponse> SearchLocationsAsync(
        [Query] string name,
        [Query] int count = 10,
        [Query] string language = "en",
        [Query] string format = "json",
        CancellationToken cancellationToken = default);

    [Get("https://geocoding-api.open-meteo.com/v1/reverse")]
    Task<OpenMeteoGeocodingResponse> ReverseGeocodeAsync(
        [Query] double latitude,
        [Query] double longitude,
        [Query] int count = 1,
        [Query] string language = "en",
        CancellationToken cancellationToken = default);
}
