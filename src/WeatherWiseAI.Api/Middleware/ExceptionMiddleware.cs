namespace WeatherWiseAI.Api.Middleware;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WeatherWiseAI.Shared.Common;
using WeatherWiseAI.Shared.Constants;
using WeatherWiseAI.Shared.Exceptions;
using FluentValidation;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (ex is OperationCanceledException || ex.InnerException is OperationCanceledException)
        {
            _logger.LogInformation("Client cancelled the request.");
            context.Response.StatusCode = 499; // Client Closed Request
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = HttpStatusCode.InternalServerError;
        var errors = new List<string> { "An unexpected error occurred." };

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                break;
            case AppException appException:
                statusCode = HttpStatusCode.BadRequest;
                if (appException.ErrorCode == AppConstants.ErrorCodes.NotFound)
                    statusCode = HttpStatusCode.NotFound;
                else if (appException.ErrorCode == AppConstants.ErrorCodes.Unauthorized)
                    statusCode = HttpStatusCode.Unauthorized;
                errors = new List<string> { appException.Message };
                break;
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                errors = new List<string> { "Unauthorized access." };
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.ErrorResponse(errors);
        
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(json);
    }
}
