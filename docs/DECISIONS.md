# Architectural Decision Records (ADRs)

This document records the major architectural and technological decisions made for WeatherWiseAI-API.

## Why Open-Meteo?
**Decision**: Use Open-Meteo for weather data.
**Reasoning**: Open-Meteo requires no API key for non-commercial use, offers robust historical and forecast data, and responds with clean, predictable JSON. It is highly reliable for stateless integrations.

## Why Gemini?
**Decision**: Use Google Gemini API for AI generation.
**Reasoning**: Gemini provides excellent natural language processing capabilities, fast inference times, and cost-effective pricing tiers for text generation, making it ideal for summarizing weather data into insights.

## Why No Database?
**Decision**: Exclude traditional relational/NoSQL databases.
**Reasoning**: The application's sole purpose is to act as a proxy and aggregator between the client, Open-Meteo, and Gemini. By remaining purely stateless, we drastically reduce infrastructure complexity, avoid data privacy issues, and enable infinite horizontal scaling.

## Why Clean Architecture?
**Decision**: Adopt Clean Architecture.
**Reasoning**: Clean Architecture separates business logic from infrastructure concerns. This allows us to easily swap out external providers (e.g., switching from Gemini to OpenAI, or Open-Meteo to AccuWeather) by simply writing a new Infrastructure implementation, without touching the Application layer.

## Why DTO Mapping?
**Decision**: Use AutoMapper to map between Domain Models and DTOs.
**Reasoning**: Prevents over-posting attacks and decoupling internal domain representations from external API contracts. It ensures controllers only expose exactly what the client needs to see.

## Why Refit?
**Decision**: Use Refit for external HTTP calls.
**Reasoning**: Refit turns REST APIs into live interfaces. It reduces HTTP boilerplate, makes external calls strongly typed, and integrates natively with ASP.NET Core `IHttpClientFactory`.

## Why FluentValidation?
**Decision**: Use FluentValidation for DTO validation.
**Reasoning**: Separates validation rules from model classes, keeping DTOs clean. It provides a fluent, easily readable syntax for complex validation logic and integrates seamlessly with ASP.NET Core middleware.
