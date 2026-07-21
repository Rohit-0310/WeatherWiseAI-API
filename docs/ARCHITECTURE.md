# Architecture

The WeatherWiseAI-API is built using **Clean Architecture**. This approach ensures separation of concerns, testability, and independence from external frameworks and services.

## Clean Architecture Overview

The solution is divided into concentric layers. The overriding rule is that **dependencies point inward**. Inner layers know nothing about outer layers.

```
API Layer
    ↓
Infrastructure Layer
    ↓
Application Layer
    ↓
Domain Layer
```

*(The `Shared` project acts as a cross-cutting concern accessible by all layers.)*

## Layer Responsibilities

### 3. Application Layer (`WeatherWiseAI.Application`)

Contains business logic, domain models, services, interfaces, CQRS elements, and mapping logic.

**Key Modules:**
*   **Weather Module:** Orchestrates external calls, validates incoming requests, and handles core domain logic (`WeatherService`, `ForecastService`, `HourlyService`).
*   **Location Module:** Connects Geocoding API to resolve latitudes, longitudes, and string queries.
*   **WeatherContext (Shared):** A deterministic, provider-agnostic aggregation layer (`WeatherContextBuilder`) that merges all weather, astronomy, and location data into a normalized single context graph and computes deterministic `AIHints`.
*   **AI Module:** Contains prompt builders (`IPromptBuilder`), insights models, and abstractions ready for Gemini integration.

**Rules:**
*   Defines interfaces for external dependencies (e.g., `IWeatherProvider`, `ILocationProvider`).

### Domain (`WeatherWiseAI.Domain`)
Contains enterprise-wide business rules, models, and domain exceptions. It has no external dependencies.
- **Models**: `WeatherCondition`, `AiInsight`.
- **Exceptions**: `DomainValidationException`.

### Infrastructure (`WeatherWiseAI.Infrastructure`)
Contains concrete implementations of the Application interfaces. It manages all external HTTP communication.
- **HTTP Clients**: Refit interfaces for Open-Meteo and Gemini.
- **Prompt Builders**: Logic for constructing the exact strings sent to the Gemini API.

### API (`WeatherWiseAI.Api`)
The entry point of the application. It handles HTTP requests, routes them to Application services, and returns HTTP responses.
- **Controllers**: Map HTTP requests to Application logic.
- **Middleware**: Global exception handling.
- **Configuration**: Dependency Injection setup.

## Dependency Rules
- `Api` references `Application` and `Infrastructure`.
- `Infrastructure` references `Application`.
- `Application` references `Domain`.
- `Domain` references nothing.
- `Shared` can be referenced by any project.

## Request Flow

1. **Client** sends HTTP Request to `API`.
2. **Controller** receives the request.
3. Request DTO is validated via **FluentValidation** pipeline.
4. **Controller** invokes a service in the **Application Layer**.
5. **Application Service** orchestrates the workflow.
6. **Application Service** calls an interface implemented in the **Infrastructure Layer** to fetch weather data.
7. **Application Service** calls another interface to generate AI insights based on the weather data.
8. **Application Service** aggregates data into Domain models.
9. **AutoMapper** maps Domain models to Response DTOs.
10. **Controller** returns HTTP Response to the **Client**.

## API Flow
All external communications (Open-Meteo, Gemini) are managed via HTTP using **Refit** within the Infrastructure layer.
- `IOpenMeteoClient` handles weather queries.
- `IGeminiClient` handles prompt submissions.

## AI Flow
1. Fetch raw weather metrics (temperature, wind, precipitation).
2. Pass raw data to a Prompt Builder.
3. Send the structured prompt to the Google Gemini API.
4. Parse the Gemini JSON/Text response.
5. Return the generated insight to the Application layer.

## Prompt Builder Flow
The Infrastructure layer contains a dedicated component for building prompts.
- Separates text generation logic from HTTP logic.
- Uses string interpolation and templates to ensure consistent requests to Gemini.

## DTO Mapping
- Mapping happens strictly between `Domain` and `API` boundaries.
- **AutoMapper** is configured in the Application layer.
- Infrastructure receives and returns DTOs/Domain models as defined by Application interfaces.

## Validation
- Handled exclusively by **FluentValidation** in the Application layer.
- A request is rejected before executing business logic if it fails validation.

## Naming Conventions
- **Controllers**: `<Entity>Controller`
- **Services**: `<Feature>Service` (Application), `<Integration>Client` (Infrastructure)
- **DTOs**: `<Action>Request`, `<Action>Response`
