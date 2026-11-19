# API deployment

Publish with the .NET 10 SDK and run the published DLL with the ASP.NET Core 10 runtime.

```sh
dotnet publish src/WeatherWiseAI.Api -c Release -o artifacts/api
dotnet artifacts/api/WeatherWiseAI.Api.dll
```

The example publishes a framework-dependent artifact. No deployment manifests or CI deployment workflow are committed. Configure the host's HTTPS listener or TLS termination and validate HTTPS redirects in the deployed topology. Expose `/health` for host checks. Swagger UI at `/` and its JSON are Development only. Use a writable log directory or configure collection of console/file logs and retention.

| Environment variable | Use |
| --- | --- |
| `ASPNETCORE_ENVIRONMENT` | Host environment. The HTTPS launch profile selects Development |
| `Gemini__ApiKey` | Server-only provider secret required for AI calls |
| `Gemini__ModelName`, `Gemini__BaseUrl` | Override checked-in Gemini model and provider URL |
| `OpenMeteo__BaseUrl`, `OpenMeteo__TimeoutSeconds` | Forecast base URL and Refit HttpClient timeout |
| `ASPNETCORE_URLS` | Standard ASP.NET Core hosting setting when configuring listeners outside launch profiles |

ASP.NET Core loads `appsettings.json`, environment-specific settings and environment overrides. Supply the key through the deployment platform's secret injection. Check provider terms, quotas, egress and model availability before hosting. Geocoding and air-quality hosts are rewritten in code. BigDataCloud is also selected in code.

Set `Cors__AllowedOrigins__0` to the exact Web origin. Set the frontend build variable `NEXT_PUBLIC_WEATHER_API_URL` to the API base including /api/v1. Public quota protection and hosting validation remain pending.

The local smoke test logged an AutoMapper license warning. Review the applicable dependency terms before hosting and keep any license credential private. The repository's MIT license covers the project. [AutoMapper licensing guidance](https://docs.automapper.io/en/latest/License-configuration.html)

CoordinateSharp 3.4.1.1 includes AGPLv3 and commercial licensing options. Confirm the applicable license for the intended distribution or hosted service.
