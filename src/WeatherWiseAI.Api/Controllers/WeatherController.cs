namespace WeatherWiseAI.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherWiseAI.Application.Weather.DTOs;
using WeatherWiseAI.Application.Weather.Services;
using WeatherWiseAI.Shared.Common;

using WeatherWiseAI.Application.Weather.Forecast.Interfaces;
using WeatherWiseAI.Application.Weather.Forecast.Requests;
using WeatherWiseAI.Application.Weather.Forecast.Responses;

using WeatherWiseAI.Application.Weather.Hourly.Interfaces;
using WeatherWiseAI.Application.Weather.Hourly.Requests;
using WeatherWiseAI.Application.Weather.Hourly.Responses;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly IForecastService _forecastService;
    private readonly IHourlyService _hourlyService;
    private readonly WeatherWiseAI.Application.Weather.Astronomy.Interfaces.IAstronomyService _astronomyService;

    public WeatherController(
        IWeatherService weatherService, 
        IForecastService forecastService, 
        IHourlyService hourlyService,
        WeatherWiseAI.Application.Weather.Astronomy.Interfaces.IAstronomyService astronomyService)
    {
        _weatherService = weatherService;
        _forecastService = forecastService;
        _hourlyService = hourlyService;
        _astronomyService = astronomyService;
    }

    /// <summary>
    /// Gets the current weather for a specific location.
    /// </summary>
    /// <param name="request">The location and preferences for the weather data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current weather details.</returns>
    [HttpGet("current")]
    [ProducesResponseType(typeof(ApiResponse<CurrentWeatherResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<CurrentWeatherResponse>>> GetCurrentWeather(
        [FromQuery] CurrentWeatherRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetCurrentWeatherAsync(request, cancellationToken);
        
        return Ok(ApiResponse<CurrentWeatherResponse>.SuccessResponse(response));
    }

    /// <summary>
    /// Gets the 7-day weekly weather forecast for a specific location.
    /// </summary>
    /// <param name="request">The location and preferences for the forecast data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The weekly weather forecast.</returns>
    [HttpGet("forecast")]
    [ProducesResponseType(typeof(ApiResponse<ForecastResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<ForecastResponseDto>>> GetWeeklyForecast(
        [FromQuery] ForecastRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _forecastService.GetWeeklyForecastAsync(request, cancellationToken);
        
        return Ok(ApiResponse<ForecastResponseDto>.SuccessResponse(response));
    }

    /// <summary>
    /// Gets the hourly weather forecast for a specific location up to a specified number of hours.
    /// </summary>
    /// <param name="request">The location, hours, and preferences for the forecast data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The hourly weather forecast.</returns>
    [HttpGet("hourly")]
    [ProducesResponseType(typeof(ApiResponse<HourlyResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<HourlyResponseDto>>> GetHourlyForecast(
        [FromQuery] HourlyRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _hourlyService.GetHourlyForecastAsync(request, cancellationToken);
        
        return Ok(ApiResponse<HourlyResponseDto>.SuccessResponse(response));
    }

    /// <summary>
    /// Gets astronomical data (Sunrise, Sunset, Moon Phase) for a specific location.
    /// </summary>
    /// <param name="request">The location parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The astronomy data.</returns>
    [HttpGet("astronomy")]
    [ProducesResponseType(typeof(ApiResponse<WeatherWiseAI.Application.Weather.Astronomy.Responses.AstronomyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<WeatherWiseAI.Application.Weather.Astronomy.Responses.AstronomyResponse>>> GetAstronomy(
        [FromQuery] WeatherWiseAI.Application.Weather.Astronomy.Requests.AstronomyRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _astronomyService.GetAstronomyDataAsync(request, cancellationToken);
        
        return Ok(ApiResponse<WeatherWiseAI.Application.Weather.Astronomy.Responses.AstronomyResponse>.SuccessResponse(response));
    }
}
