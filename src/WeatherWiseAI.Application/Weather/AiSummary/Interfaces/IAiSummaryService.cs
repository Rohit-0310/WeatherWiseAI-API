using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.AiSummary.Requests;
using WeatherWiseAI.Application.Weather.AiSummary.Responses;
using WeatherWiseAI.Shared.Common;

namespace WeatherWiseAI.Application.Weather.AiSummary.Interfaces;

public interface IAiSummaryService
{
    Task<Result<AiSummaryResponse>> GenerateSummaryAsync(AiSummaryRequest request, CancellationToken cancellationToken = default);
}
