# AI Assistant Guidelines

This document provides strict instructions and context for AI coding assistants (ChatGPT, Codex, Claude Code, Cursor, Antigravity CLI, Gemini CLI, GitHub Copilot) working on the **WeatherWiseAI-API** repository.

## Architecture

This project strictly adheres to **Clean Architecture**.

- **Domain Layer**: Pure C# logic. Contains domain models and exceptions. No external dependencies.
- **Application Layer**: Contains business logic, interfaces, validators, and DTOs. Depends only on Domain.
- **Infrastructure Layer**: Implements Application interfaces. Handles all HTTP integrations (Open-Meteo via Refit, Google Gemini). Depends on Application and Domain.
- **API Layer**: ASP.NET Core Web API controllers, DI registration, and middleware. Depends on Application and Infrastructure.
- **Shared Layer**: Common utilities used across multiple layers.

**Note**: There is NO database. All external data comes from HTTP APIs.

## Dependency Rules

- Dependencies point **inward** toward the Domain layer.
- `WeatherWiseAI.Api` -> `WeatherWiseAI.Application`, `WeatherWiseAI.Infrastructure`
- `WeatherWiseAI.Infrastructure` -> `WeatherWiseAI.Application`
- `WeatherWiseAI.Application` -> `WeatherWiseAI.Domain`
- `WeatherWiseAI.Domain` -> No dependencies
- `WeatherWiseAI.Shared` -> No architectural dependencies, used everywhere safely.

## Folder Responsibilities

### src/WeatherWiseAI.Api
- `/Controllers`: HTTP endpoints.
- `/Middleware`: Global error handling and logging.
- `/Extensions`: Service collection extensions for DI.

### src/WeatherWiseAI.Application
- `/DTOs`: Data Transfer Objects for request/response.
- `/Interfaces`: Abstractions for external services (e.g., `IWeatherService`, `IAiService`).
- `/Mappings`: AutoMapper profiles.
- `/Validators`: FluentValidation rules for incoming requests.
- `/Services`: Core business logic coordinating infrastructure calls.

### src/WeatherWiseAI.Domain
- `/Models`: Core domain representations of weather data and insights.
- `/Exceptions`: Domain-specific exception definitions.

### src/WeatherWiseAI.Infrastructure
- `/HttpClients`: Refit interfaces and delegating handlers.
- `/Services`: Concrete implementations of Application interfaces.
- `/PromptBuilders`: Logic for constructing prompts to send to Gemini.

### src/WeatherWiseAI.Shared
- `/Constants`: Magic strings, error codes.
- `/Utils`: Helper functions.

## Naming Conventions

- **Interfaces**: Prefix with `I` (e.g., `IWeatherService`).
- **Classes/Structs**: PascalCase.
- **Private Fields**: camelCase prefixed with `_` (e.g., `_weatherService`).
- **Constants**: PascalCase (e.g., `MaxRetryCount`).
- **DTOs**: Suffix with `Request` or `Response` (e.g., `WeatherRequest`, `WeatherResponse`).
- **Controllers**: Suffix with `Controller` (e.g., `WeatherController`).
- **Refit Clients**: Suffix with `Client` (e.g., `IOpenMeteoClient`).

## How New Features Should Be Implemented

1. **Domain**: Define necessary domain models.
2. **Application**: 
   - Define DTOs for the feature.
   - Define Validators for the request DTOs.
   - Define Interfaces that Infrastructure needs to implement.
   - Implement the Application Service that executes the business use case.
3. **Infrastructure**: Implement the HTTP integrations required by the Application interface.
4. **API**: Create the Controller endpoint, inject the Application service, and return standard HTTP responses. Update DI in `Program.cs` or extension methods.

## Rules That AI Assistants Must Always Follow

1. **Strict Dependency Adherence**: Never introduce a dependency that violates the inward-pointing Clean Architecture rule.
2. **No Database Logic**: Never attempt to write Entity Framework code, SQL queries, or migrations. This project does not have a database.
3. **DTOs over Entities**: Always map Domain models to DTOs before returning them from the API. Never expose Domain models directly in controllers.
4. **Use Established Libraries**: Always use `Refit` for HTTP calls, `FluentValidation` for validation, and `Serilog` for logging. Do not introduce alternative libraries for these concerns.
5. **Separation of Concerns**: Controllers should only handle HTTP routing and delegate to Application services.

## Things AI Assistants Must Never Do

- **DO NOT** add a database or ORM (like Entity Framework).
- **DO NOT** write business logic inside Controllers.
- **DO NOT** call Infrastructure directly from Controllers; always go through Application interfaces.
- **DO NOT** expose API Keys or secrets in code.
- **DO NOT** modify the standard formatting (C# conventions apply).
