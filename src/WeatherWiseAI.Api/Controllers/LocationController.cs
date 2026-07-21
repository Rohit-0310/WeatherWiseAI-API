namespace WeatherWiseAI.Api.Controllers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseAI.Application.Location.DTOs;
using WeatherWiseAI.Application.Location.Interfaces;
using WeatherWiseAI.Application.Location.Requests;
using WeatherWiseAI.Shared.Common;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Searches for locations by name using the Geocoding API.
    /// </summary>
    /// <param name="request">The search query parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of matching locations.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<List<LocationResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<List<LocationResponseDto>>>> Search(
        [FromQuery] LocationSearchRequest request,
        CancellationToken cancellationToken)
    {
        var locations = await _locationService.SearchLocationsAsync(request, cancellationToken);
        return Ok(ApiResponse<List<LocationResponseDto>>.SuccessResponse(locations));
    }

    /// <summary>
    /// Reverse geocodes latitude and longitude into a structured location.
    /// </summary>
    /// <param name="request">The coordinates to reverse geocode.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching location, or null if not found.</returns>
    [HttpGet("reverse")]
    [ProducesResponseType(typeof(ApiResponse<LocationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<LocationResponseDto>>> Reverse(
        [FromQuery] LocationReverseRequest request,
        CancellationToken cancellationToken)
    {
        var location = await _locationService.ReverseGeocodeAsync(request, cancellationToken);
        return Ok(ApiResponse<LocationResponseDto?>.SuccessResponse(location));
    }
}
