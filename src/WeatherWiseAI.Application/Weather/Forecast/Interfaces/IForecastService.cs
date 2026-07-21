namespace WeatherWiseAI.Application.Weather.Forecast.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.Forecast.Requests;
using WeatherWiseAI.Application.Weather.Forecast.Responses;

public interface IForecastService
{
    Task<ForecastResponseDto> GetWeeklyForecastAsync(ForecastRequest request, CancellationToken cancellationToken = default);
}
