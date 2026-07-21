# Project Roadmap

This document outlines the development milestones for WeatherWiseAI-API.

## Phase 1: Project Setup
- [x] Initialize ASP.NET Core 10 Web API project.
- [x] Scaffold Clean Architecture solution structure.
- [x] Configure Serilog for structured logging.
- [x] Setup global exception handling middleware.

## Phase 2: Weather Integration
- [x] Define Open-Meteo response DTOs.
- [x] Implement Refit interface for Open-Meteo API.
- [x] Create `WeatherService` in Application layer to orchestrate fetch logic.
- [x] Expose basic weather endpoints in API layer.

## Phase 3: AI Integration
- [ ] Register Google Gemini API Key in configurations.
- [ ] Implement Refit interface for Google Gemini API.
- [ ] Create `AiInsightService` in Application layer.
- [x] Create provider-independent `WeatherContext` foundation.

## Phase 4: Prompt Engineering
- [x] Create Prompt Builder abstractions (`IPromptBuilder` interfaces).
- [ ] Implement `PromptBuilder` for Gemini requests.
- [ ] Define basic weather summary templates.
- [ ] Add tone and style variations (e.g., formal, witty).
- [ ] Test prompt robustness and handle hallucinations.

## Phase 5: Testing
- [ ] Setup xUnit/NUnit test projects.
- [ ] Write unit tests for Application layer services.
- [ ] Write unit tests for Prompt Builders.
- [ ] Implement integration tests for controllers using WebApplicationFactory.

## Phase 6: Deployment
- [ ] Configure release build profiles.
- [ ] Setup environment variable management.
- [ ] Create Dockerfile.
- [ ] Setup CI/CD pipeline (e.g., GitHub Actions).

## Phase 7: Future Enhancements
- [ ] Multi-language support for AI insights.
- [ ] User preference mapping (via headers or JWT claims) to customize AI tone.
- [ ] Caching integration (Redis or InMemory) to reduce external API calls.
- [ ] Rate limiting middleware.
