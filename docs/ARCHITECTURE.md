# Architecture

WeatherWiseAI API is a stateless ASP.NET Core service with Clean Architecture style boundaries. All persistent weather and location data comes from upstream providers. The application registers no database, ORM, or weather-response cache.

| Project | Actual responsibility | Direct project references |
| --- | --- | --- |
| Api | MVC routes, model binding, exception middleware, OpenAPI, health check, CORS and composition root | Application, Infrastructure, Shared |
| Application | Feature services, DTOs, provider interfaces, validation rules, mapping and astronomy calculations | Domain, Shared |
| Infrastructure | Refit Open-Meteo integration, BigDataCloud reverse lookup and typed HttpClient Gemini service | Application, Shared |
| Domain | Reserved project, currently without domain entities | None |
| Shared | Envelopes, results, error types, constants and exceptions | None |

The browser in [WeatherWiseAI-Web](https://github.com/Rohit-0310/WeatherWiseAI-Web) sends requests through Axios to the API. Weather and location controllers delegate to scoped Application services and Infrastructure adapters. AutoMapper maps provider data to DTOs and normalizes Open-Meteo conditions. Current weather fetches forecast and air quality concurrently. The singleton condition mapper is shared. Astronomy combines Open-Meteo sunlight data with CoordinateSharp lunar calculations.

The Refit client handles forecast, air-quality and search paths with host rewriting and Polly resilience. Reverse geocoding calls BigDataCloud through a separate HttpClient and falls back to a coordinate label. The AI controller injects the typed Gemini implementation through its Application interface. It sends client-supplied weather strings rather than invoking the weather context builder. The builder and prompt interfaces are foundations for future integration.

[HTTP contract](API_GUIDELINES.md) · [Resilience and errors](ERROR_HANDLING.md) · [AI flow](PROMPT_ENGINEERING.md)

## Trace an hourly forecast

1. The browser sends `GET /api/v1/Weather/hourly` with coordinates and units. [WeatherController](../src/WeatherWiseAI.Api/Controllers/WeatherController.cs) binds an HourlyRequest and delegates to IHourlyService.
2. [HourlyService](../src/WeatherWiseAI.Application/Weather/Hourly/Services/HourlyService.cs) forwards the coordinates, hour count, units, timezone and cancellation token to IWeatherProvider. This interface isolates the use case from the provider's query vocabulary.
3. [OpenMeteoWeatherProvider](../src/WeatherWiseAI.Infrastructure/OpenMeteo/Services/OpenMeteoWeatherProvider.cs) requests hourly temperature, weather code, wind speed and precipitation probability through Refit. It passes the hour count as forecastHours, substitutes `auto` for a missing timezone, and requests Fahrenheit and mph for Imperial.
4. Infrastructure maps the provider response into ProviderHourlyWeatherDto. Application maps that DTO into HourlyResponseDto and sets the units from the request. The controller wraps it with ApiResponse.SuccessResponse. The Web hourly panel reads `data.hours`.

## Trace an AI suggestion

[AiSummaryController](../src/WeatherWiseAI.Api/Controllers/AiSummaryController.cs) accepts the browser's weather snapshot and calls IAiSummaryService. [GeminiAiSummaryService](../src/WeatherWiseAI.Infrastructure/Gemini/Services/GeminiAiSummaryService.cs) constructs the prompt, makes the outbound request and translates the candidate text to an AiSummaryResponse. The controller unwraps a successful Result and returns the three fields directly. For a failure Result it returns BadRequest with an error member. See the [prompt guide](PROMPT_ENGINEERING.md) for parsing behavior.
