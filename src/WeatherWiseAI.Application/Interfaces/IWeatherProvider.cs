namespace WeatherWiseAI.Application.Interfaces;

using WeatherWiseAI.Application.DTOs;

public interface IWeatherProvider
{
    Task<WeatherResponseDto> GetCurrentWeatherAsync(double latitude, double longitude, string units, string? timezone, CancellationToken cancellationToken = default);
    Task<WeeklyWeatherDto> GetWeeklyForecastAsync(double latitude, double longitude, string units, string? timezone, CancellationToken cancellationToken = default);
    Task<ProviderHourlyWeatherDto> GetHourlyForecastAsync(double latitude, double longitude, int hours, string units, string? timezone, CancellationToken cancellationToken = default);
}
