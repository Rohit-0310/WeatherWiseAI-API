# Deployment Guide

This document covers the configuration and deployment strategy for WeatherWiseAI-API.

## Configuration
Configuration is managed via ASP.NET Core's `appsettings.json` and environment variables.

### `appsettings.json`
Contains non-sensitive, environment-agnostic configuration (e.g., Serilog setup, default API routes).

### `appsettings.Development.json`
Overrides for local development, enabling detailed logging and Swagger UI.

## Environment Variables
Sensitive data and environment-specific endpoints must be injected via Environment Variables.

Required variables in Production:
- `ASPNETCORE_ENVIRONMENT`: Set to `Production`.
- `Gemini__ApiKey`: The secure API key for Google Gemini.
- `OpenMeteo__BaseUrl`: The base URL for the Open-Meteo API.

## Swagger
- Swagger (Swashbuckle) is enabled in `Development` environments for easy testing.
- In `Production`, Swagger is disabled to prevent exposing API contracts publicly unless explicitly required.

## Release Build
To prepare the application for deployment, build it in Release mode:

```bash
dotnet publish src/WeatherWiseAI.Api/WeatherWiseAI.Api.csproj -c Release -o ./out
```

## Production Deployment
As a stateless web API, WeatherWiseAI-API is highly container-friendly.

1. **Docker**: A standard .NET ASP.NET Core Dockerfile should be used.
2. **Hosting**: The container can be hosted on any cloud provider supporting containers (e.g., Azure App Service, AWS ECS, Google Cloud Run).
3. **Scaling**: Since there is no database, you can scale horizontally infinitely behind a load balancer. Ensure external API rate limits (Gemini/Open-Meteo) are monitored when scaling.
