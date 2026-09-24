using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseAI.Application.Weather.AiSummary.Interfaces;
using WeatherWiseAI.Application.Weather.AiSummary.Requests;
using WeatherWiseAI.Application.Weather.AiSummary.Responses;

namespace WeatherWiseAI.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AiSummaryController : ControllerBase
{
    private readonly IAiSummaryService _aiSummaryService;

    public AiSummaryController(IAiSummaryService aiSummaryService)
    {
        _aiSummaryService = aiSummaryService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AiSummaryResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GenerateSummary([FromBody] AiSummaryRequest request)
    {
        var result = await _aiSummaryService.GenerateSummaryAsync(request);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new { Error = result.Error });
    }
}
