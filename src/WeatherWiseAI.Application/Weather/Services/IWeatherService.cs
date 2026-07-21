namespace WeatherWiseAI.Application.Weather.Services;

using WeatherWiseAI.Application.Weather.DTOs;

public interface IWeatherService
{
    Task<CurrentWeatherResponse> GetCurrentWeatherAsync(CurrentWeatherRequest request, CancellationToken cancellationToken = default);
}
