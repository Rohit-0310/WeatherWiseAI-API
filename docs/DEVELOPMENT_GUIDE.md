# Development guide

Start with the [.NET 10 local setup](../README.md#run-locally). Inspect the feature's request, response, service, provider interface and mappings before changing its contract. Add transport behavior to API, use cases and DTOs to Application, and external adapters to Infrastructure. Keep registrations in the existing DependencyInjection or provider extension files. See [coding standards](CODING_STANDARDS.md).

## Checks

```sh
dotnet restore WeatherWiseAI.slnx
dotnet build WeatherWiseAI.slnx -c Release
dotnet test WeatherWiseAI.slnx --list-tests
```

Run `dotnet test WeatherWiseAI.slnx -c Release` to execute the xUnit HTTP contract suite. For manual verification, start the HTTPS development profile, check `/health` and Swagger, then exercise all GET routes with known coordinates. Check metric/imperial values, search and reverse results, malformed inputs, and frontend CORS. Run AI generation with a privately configured key and check both success and failure response shapes. Live provider checks require outbound access. Use synthetic weather values and record outcomes without provider keys or sensitive logs.

## Extend an existing weather feature

Suppose an hourly panel needs another measurement. Start by defining its unit, missing-value behavior and response field with the Web consumer. Trace the [hourly request](ARCHITECTURE.md#trace-an-hourly-forecast) in both directions before editing.

1. **Application contract.** Locate the hourly element DTO under `Weather/Hourly/Responses`. Add query options to the feature request only when the caller needs to control them. Preserve existing defaults so old callers can omit new options.
2. **Provider integration.** Inspect IWeatherProvider and the intermediate provider DTO. In Infrastructure, extend the Open-Meteo field list and deserialization model, then check the mapping against a response with missing or shorter hourly arrays.
3. **Application mapping.** Map the new measurement into the public DTO. Check both unit systems and verify that timestamps and measurements belong to the same hour.
4. **HTTP boundary.** For a new action, follow the existing controller's versioned route, binding and return conventions. Verify its schema in Development Swagger. Adding a field to an existing response usually requires no new controller.
5. **Registration and validation.** Feature services are scoped in [Application DI](../src/WeatherWiseAI.Application/DependencyInjection.cs). The hourly registration is equivalent to `services.AddScoped<IHourlyService, HourlyService>()` with feature namespaces imported. Provider client options, handlers and resilience belong in Infrastructure's integration extensions. Assembly scanning discovers AbstractValidator classes. Test the HTTP path separately to establish enforcement.
6. **Compatibility check.** Exercise a normal request and malformed input. Compare the response with the Web service's assumptions, including nulls and error bodies. Record the observed result alongside the change.
