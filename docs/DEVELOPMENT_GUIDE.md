# Development guide

Start with the [.NET 10 local setup](../README.md#run-locally). Inspect the feature's request, response, service, provider interface and mappings before changing its contract. Add transport behavior to API, use cases and DTOs to Application, and external adapters to Infrastructure. Keep registrations in the existing DependencyInjection or provider extension files. See [coding standards](CODING_STANDARDS.md).

## Checks

```sh
dotnet restore WeatherWiseAI.slnx
dotnet build WeatherWiseAI.slnx -c Release
dotnet test WeatherWiseAI.slnx --list-tests
```

The solution has no test project, so an empty discovery or successful `dotnet test` exit does not establish automated coverage. For manual verification, start the HTTPS development profile, check `/health` and Swagger, then exercise all GET routes with known coordinates. Check metric/imperial values, search and reverse results, malformed inputs, and frontend CORS. Run AI generation with a privately configured key and check both success and failure response shapes. Live provider checks require outbound access. Use synthetic weather values and record outcomes without provider keys or sensitive logs.
