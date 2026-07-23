namespace WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

using Refit;
using WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

public interface IOpenMeteoClient
{
    [Get("/v1/forecast")]
    Task<OpenMeteoResponse> GetForecastAsync(
        [Query] double latitude, 
        [Query] double longitude, 
        [AliasAs("current_weather")] bool currentWeather = true,
        [AliasAs("current")] string? current = null,
        [AliasAs("temperature_unit")] string? temperatureUnit = null,
        [AliasAs("windspeed_unit")] string? windSpeedUnit = null,
        [AliasAs("timezone")] string? timezone = null,
        [AliasAs("daily")] string? daily = null,
        [AliasAs("hourly")] string? hourly = null,
        [AliasAs("forecast_hours")] int? forecastHours = null,
        [AliasAs("start_date")] string? startDate = null,
        [AliasAs("end_date")] string? endDate = null,
        CancellationToken cancellationToken = default);

    [Get("/v1/search")]
    Task<OpenMeteoGeocodingResponse> SearchLocationsAsync(
        [Query] string name,
        [Query] int count = 10,
        [Query] string language = "en",
        [Query] string format = "json",
        CancellationToken cancellationToken = default);

    [Get("/v1/reverse")]
    Task<OpenMeteoGeocodingResponse> ReverseGeocodeAsync(
        [Query] double latitude,
        [Query] double longitude,
        [Query] int count = 1,
        [Query] string language = "en",
        CancellationToken cancellationToken = default);

    [Get("/v1/air-quality")]
    Task<OpenMeteoAirQualityResponse> GetAirQualityAsync(
        [Query] double latitude,
        [Query] double longitude,
        [AliasAs("current")] string current = "european_aqi,us_aqi",
        CancellationToken cancellationToken = default);
}
