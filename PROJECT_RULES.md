# 📖 WeatherWiseAI API - Project Rules

Version: 1.0

This document defines the mandatory engineering rules for the WeatherWiseAI API project.

These rules apply to all contributors, including developers and AI coding assistants.

---

# 1. General Principles

- Follow Clean Architecture.
- Follow SOLID principles.
- Keep the codebase modular.
- Keep the code readable.
- Prioritize maintainability over shortcuts.
- Avoid unnecessary complexity.
- Never introduce breaking architectural changes without documenting them.

---

# 2. Layer Responsibilities

## API

Allowed

- Controllers
- Middleware
- Dependency Injection
- Configuration
- Filters

Not Allowed

- Business Logic
- HTTP client calls
- Prompt construction
- Data transformation
- External API calls

---

## Application

Allowed

- Business Logic
- DTOs
- Validators
- Mapping
- Interfaces
- Prompt Builders
- Use Cases

Not Allowed

- HttpClient
- Refit
- External APIs
- Configuration access

---

## Infrastructure

Allowed

- Open-Meteo integration
- Google Gemini integration
- Refit
- HTTP clients
- Polly
- Logging
- Caching

Not Allowed

- Business rules
- Validation logic

---

## Domain

Allowed

- Models
- Value Objects
- Enums
- Domain Exceptions

Not Allowed

- Dependencies on any other project

---

## Shared

Allowed

- Constants
- Helpers
- Result Wrappers
- Extensions

---

# 3. Dependency Rules

Allowed

API

↓

Application

↓

Infrastructure

↓

External Services

Domain has no dependencies.

Application must never reference API.

Infrastructure must never reference API.

Domain must never reference Infrastructure.

---

# 4. Controllers

Controllers must remain thin.

Controllers may only

- Validate request
- Call Application service
- Return response

Controllers must never

- Build prompts
- Call Gemini
- Call Open-Meteo
- Parse JSON
- Implement business logic

---

# 5. DTO Rules

Never expose

- Open-Meteo models
- Gemini models

Always map

External Models

↓

Internal DTOs

↓

API Responses

---

# 6. Validation

All incoming requests must use FluentValidation.

Never validate inside controllers.

Never use manual validation.

---

# 7. Prompt Engineering

Every AI prompt must live inside

Application/PromptBuilders

Never create prompts inside

Controllers

Services

Infrastructure

---

# 8. Error Handling

All exceptions must be handled globally.

Controllers must never contain try/catch unless absolutely necessary.

Return standardized error responses.

---

# 9. Logging

Log

- Requests
- External API failures
- AI failures
- Validation failures

Never log

- API keys
- Secrets
- Sensitive information

---

# 10. Configuration

Never hardcode

URLs

API Keys

Model names

Timeouts

Everything must come from configuration.

---

# 11. Naming

Interfaces

IWeatherService

Classes

WeatherService

DTO

WeatherSummaryDto

Validators

WeatherSummaryValidator

Controllers

WeatherController

---

# 12. Async

Use async/await everywhere.

Avoid synchronous blocking.

Never use .Result or .Wait().

---

# 13. Dependency Injection

All services must be registered through centralized DI.

Avoid service locator patterns.

---

# 14. External APIs

Only Infrastructure may communicate with

Open-Meteo

Gemini

No other layer may perform HTTP requests.

---

# 15. Response Format

Always return standardized responses.

Avoid anonymous objects.

---

# 16. Code Quality

Every public method must

- Have a clear purpose
- Follow Single Responsibility
- Be easily testable

---

# 17. Documentation

Every new feature must update

README

ROADMAP

Architecture (if needed)

API documentation

---

# 18. Git

Feature branches

feature/weather

feature/theme

feature/ai-summary

Commit messages follow Conventional Commits.

---

# 19. Testing

Business logic should be testable.

Avoid tightly coupled code.

Use interfaces.

---

# 20. AI Assistant Rules

AI assistants must

- Respect architecture
- Follow naming conventions
- Update documentation
- Never generate duplicate code
- Never violate dependency rules
- Prefer maintainability over shortcuts

These rules are mandatory.