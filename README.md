# WeatherWiseAI-API

## Project Overview

WeatherWiseAI-API is an intelligent weather information service built on ASP.NET Core 10. It utilizes a Clean Architecture approach to aggregate real-time weather data from the Open-Meteo API and generate natural language weather summaries and insights using the Google Gemini API. This API is designed to be highly scalable, enterprise-ready, and fully stateless, with no internal database required.

## Features

- **Real-Time Weather Data**: Fetches accurate and up-to-date weather information using the Open-Meteo API.
- **AI-Powered Insights**: Leverages Google Gemini API to generate human-readable summaries and personalized weather insights.
- **Stateless Architecture**: Operates without a database, relying purely on external service integrations, reducing complexity and increasing horizontal scalability.
- **Robust Validation**: Employs FluentValidation for robust incoming request validation.
- **Clean Architecture**: Ensures separation of concerns, high maintainability, and testability.

## Technology Stack

- **Framework**: ASP.NET Core 10 Web API
- **Language**: C#
- **Architecture**: Clean Architecture
- **HTTP Client**: Refit
- **Validation**: FluentValidation
- **Object Mapping**: AutoMapper
- **Logging**: Serilog
- **External APIs**: Open-Meteo API, Google Gemini API
- **Data Storage**: None (Stateless)

## Solution Structure

The project follows a standard Clean Architecture structure:

- `WeatherWiseAI.Api`: Contains controllers, middleware, and application configuration.
- `WeatherWiseAI.Application`: Contains business logic, interfaces, CQRS handlers (if applicable), and DTOs.
- `WeatherWiseAI.Domain`: Contains core business models, entities, and enums.
- `WeatherWiseAI.Infrastructure`: Owns all HTTP integrations (Open-Meteo, Gemini) and external service implementations.
- `WeatherWiseAI.Shared`: Contains common utilities, constants, and extensions used across layers.

## Getting Started

### Prerequisites

- .NET 10 SDK
- An API Key for Google Gemini API

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd WeatherWiseAI-API
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Environment Variables

The application relies on the following environment variables for configuration. Create an `appsettings.Development.json` or configure your environment accordingly.

- `Gemini:ApiKey`: Your Google Gemini API Key.
- `OpenMeteo:BaseUrl`: The base URL for the Open-Meteo API (e.g., `https://api.open-meteo.com`).

## Running the API

To run the application locally:

```bash
cd src/WeatherWiseAI.Api
dotnet run
```

The Swagger UI will be available at `https://localhost:<port>/swagger` (if configured in development).

## Documentation Index

Comprehensive documentation can be found in the `/docs` directory:

- [Architecture Guide](docs/ARCHITECTURE.md)
- [API Guidelines](docs/API_GUIDELINES.md)
- [Development Guide](docs/DEVELOPMENT_GUIDE.md)
- [Prompt Engineering](docs/PROMPT_ENGINEERING.md)
- [Coding Standards](docs/CODING_STANDARDS.md)
- [Error Handling](docs/ERROR_HANDLING.md)
- [Deployment](docs/DEPLOYMENT.md)
- [Roadmap](docs/ROADMAP.md)
- [Decisions](docs/DECISIONS.md)
- [Changelog](docs/CHANGELOG.md)

## Future Roadmap

Please refer to the [Roadmap](docs/ROADMAP.md) document for upcoming milestones, including advanced prompt engineering and expanded weather integration features.