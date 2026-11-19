# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

- Enforce registered request rules with structured 400 errors. Missing AQI now returns null. Configure browser origins per environment. Require three nonblank AI fields and send the provider credential in an HTTP header. Add xUnit HTTP contract checks and CI.

- Restore contributor workflows, request walkthroughs and the inventory of earlier documentation sections.

- Document current astronomy, reverse geocoding and Gemini flows, validation coverage, response formats and deployment limits.
- Add contributor and security guidance, a cross-repository audit and a draft v1.0.0 release assessment.

## [0.1.0] - Historical project milestone

### Added
- Project initialization.
- Scaffolding of Clean Architecture directories and projects.
- Documentation structure established (Architecture, Guidelines, Roadmap).
- Open-Meteo Integration (Refit client, resiliency pipelines).
- Current Weather feature (API endpoint, service, mappings).
- Weekly Forecast feature (7-day structured forecast API, normalized weather codes).
- Hourly forecast feature. The current API defaults to 24 hours and accepts 1 to 48.
- Centralized WeatherConditionMapper (Shared DTOs & Services).
- Location module for search and reverse geocoding. Current adapters use Open-Meteo for search and BigDataCloud for reverse lookup.
- WeatherContext foundation (Unified cross-service data builder for AI contexts, AI hints evaluation).
- Provider-independent WeatherConditionMapper (Normalizes Open-Meteo codes to standardized text strings).
- Initial scaffolding for AI module (Prompt Builder abstractions, structural directories).
