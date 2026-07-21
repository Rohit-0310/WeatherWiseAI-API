namespace WeatherWiseAI.Application.Weather.Shared.Services;

using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.Shared.WeatherContext;

public interface IWeatherContextBuilder
{
    Task<WeatherContext> BuildContextAsync(double latitude, double longitude, string units, string language, CancellationToken cancellationToken = default);
}
