# API roadmap

## Implemented

Current weather and AQI, hourly and daily forecasts, location search and reverse geocoding, astronomy, Gemini summaries, Development Swagger, host health check, Open-Meteo resilience and structured logging are present. See the [API reference](API_GUIDELINES.md).

## Proposed before v1.0.0

- Wire and test validation, clarify temperature/wind units in AI inputs, verify timezone handling and agree on error contracts.
- Address dependency advisories, review logging redaction, configure CORS for target origins, and decide how to protect public provider quotas.
- Add unit, contract and provider-failure tests with deterministic fixtures. Add CI and verify deployment and provider access.
- Define compatibility and release policy for the public API.

Provider caching, richer weather context for AI, and additional weather products remain proposals. See [draft release notes](RELEASE_NOTES_V1.md).
