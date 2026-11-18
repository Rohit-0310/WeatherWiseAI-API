# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
- Hourly Forecast feature (24-48hr structured API).
- Centralized WeatherConditionMapper (Shared DTOs & Services).
- Location Module (Search & Reverse geocoding via Open-Meteo).
- WeatherContext foundation (Unified cross-service data builder for AI contexts, AI hints evaluation).
- Provider-independent WeatherConditionMapper (Normalizes Open-Meteo codes to standardized text strings).
- Initial scaffolding for AI module (Prompt Builder abstractions, structural directories).
