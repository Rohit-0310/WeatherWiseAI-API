namespace WeatherWiseAI.Application.Weather.Astronomy.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.Astronomy.Requests;
using WeatherWiseAI.Application.Weather.Astronomy.Responses;

public interface IAstronomyService
{
    Task<AstronomyResponse> GetAstronomyDataAsync(AstronomyRequest request, CancellationToken cancellationToken = default);
}
