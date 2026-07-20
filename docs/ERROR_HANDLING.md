# Error Handling

WeatherWiseAI-API employs a centralized error handling strategy to ensure consistent API responses and robust logging.

## Global Exception Middleware
- We use a custom ASP.NET Core Middleware to catch all unhandled exceptions globally.
- The middleware formats the exception into a standard JSON response (see [API Guidelines](API_GUIDELINES.md)).
- Stack traces are **never** leaked to the client in production.
- Ensures a unified `500 Internal Server Error` format for unexpected crashes.

## Validation Failures
- `FluentValidation` is integrated into the request pipeline.
- If a request DTO is invalid, the pipeline short-circuits.
- Returns a `400 Bad Request` with an array of specific validation error messages.

## Integration Failures
- The API relies on Open-Meteo and Google Gemini. These external services can fail.
- Refit throws `ApiException` on non-success HTTP status codes.
- The Infrastructure layer should catch these, log the failure, and throw a custom `IntegrationException` so the Application layer understands the external service failed without knowing the HTTP details.

## Logging
- We use **Serilog** for structured logging.
- **Errors** are logged with full stack traces, request context, and correlation IDs.
- **Warnings** are logged for validation failures or transient faults.

## Retry Strategy
- Transient HTTP failures (e.g., `503 Service Unavailable`, network timeouts) when calling Open-Meteo or Gemini should be handled using **Polly**.
- A standard retry policy: Retry 3 times with exponential backoff (e.g., wait 2s, 4s, 8s).

## Fallback Strategy
- If the Gemini API fails entirely, the API should gracefully degrade.
- Instead of returning a 500 error, the API can return the raw weather data with a hardcoded fallback insight (e.g., "AI insights are currently unavailable, but here is your raw weather data.").
- If Open-Meteo fails, the request cannot be fulfilled, and a `502 Bad Gateway` should be returned indicating the upstream data provider is down.
