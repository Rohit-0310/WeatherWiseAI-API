namespace WeatherWiseAI.Application.Weather.Hourly.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.Hourly.Requests;
using WeatherWiseAI.Application.Weather.Hourly.Responses;

public interface IHourlyService
{
    Task<HourlyResponseDto> GetHourlyForecastAsync(HourlyRequest request, CancellationToken cancellationToken = default);
}
