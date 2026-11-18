# WeatherWiseAI API

WeatherWiseAI API is an ASP.NET Core 10 service for the [WeatherWiseAI web dashboard](https://github.com/Rohit-0310/WeatherWiseAI-Web). It provides current weather with European AQI, hourly and daily forecasts, location search and reverse geocoding, solar and lunar information, and on-demand Gemini weather summaries, clothing recommendations and activity suggestions.

The stateless solution uses C#, ASP.NET Core, Refit and Polly for Open-Meteo, typed HttpClient for Gemini, AutoMapper, FluentValidation rule definitions, CoordinateSharp and Serilog. No database is required. [Architecture](docs/ARCHITECTURE.md)

## Run locally

Install the .NET 10 SDK and restore dependencies. Internet access is needed for NuGet and live provider calls. A Gemini key is required only for AI generation.

```sh
dotnet restore WeatherWiseAI.slnx
dotnet dev-certs https --trust
dotnet run --project src/WeatherWiseAI.Api --launch-profile https
```

Open Swagger UI at **https://localhost:7008/** in Development. Its JSON is at `/swagger/v1/swagger.json`. The frontend expects `https://localhost:7008/api/v1` and must run at `http://localhost:3000` for the current CORS policy. Check the host with `curl https://localhost:7008/health`.

Set `Gemini__ApiKey` privately in the server process environment. Optional overrides are `Gemini__ModelName`, `Gemini__BaseUrl`, `OpenMeteo__BaseUrl` and `OpenMeteo__TimeoutSeconds`. ASP.NET Core uses double underscores for nested environment keys. The checked-in settings supply provider defaults. For configuration, logging and hosting guidance, see [deployment](docs/DEPLOYMENT.md).

## API endpoints

Business routes use `/api/v1`. They currently require no caller credentials. Gemini uses a server-side provider credential.

| Method | Path under /api/v1 | Purpose |
| --- | --- | --- |
| GET | `/Weather/current` | Current conditions, daily metrics and AQI |
| GET | `/Weather/hourly` | Hourly forecast |
| GET | `/Weather/forecast` | Daily forecast |
| GET | `/Weather/astronomy` | Solar and lunar data |
| GET | `/Location/search` | Search by name |
| GET | `/Location/reverse` | Resolve coordinates to a location |
| POST | `/AiSummary` | Generate recommendations from supplied weather strings |

Weather and location GETs return a data envelope. AI success returns a plain object. See the [API reference](docs/API_GUIDELINES.md) for parameters, examples and errors.

## Check and contribute

Run `dotnet build WeatherWiseAI.slnx -c Release`. The solution currently has no automated test project. Follow the [smoke checks](docs/DEVELOPMENT_GUIDE.md) for integration changes. A framework-dependent publish procedure and current hosting constraints are in [deployment](docs/DEPLOYMENT.md).

See the [documentation index](docs/README.md), [contribution guide](CONTRIBUTING.md), [security policy](SECURITY.md), and [draft v1.0.0 notes](docs/RELEASE_NOTES_V1.md). The project uses the [MIT License](LICENSE).
