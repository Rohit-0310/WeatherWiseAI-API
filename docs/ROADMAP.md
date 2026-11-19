# API roadmap

## Implemented

Current weather and AQI, hourly and daily forecasts, location search and reverse geocoding, astronomy, Gemini summaries, Development Swagger, host health check, Open-Meteo resilience and structured logging are present. See the [API reference](API_GUIDELINES.md).

## Proposed before v1.0.0

- Verify target-environment HTTPS, upstream failures, timezones and provider licensing.
- Decide public Gemini access and quota controls before exposing the service.
- Verify CI on a clean runner and confirm reporting and release policy.

Provider caching, richer weather context for AI, and additional weather products remain proposals. See [draft release notes](RELEASE_NOTES_V1.md).

## Earlier milestones and remaining work

| Milestone | Current position |
| --- | --- |
| Host and weather foundation | Solution layering, logging, middleware and the Open-Meteo weather paths are implemented. |
| AI and prompts | Gemini generation runs through a typed HTTP client. WeatherContext and prompt interfaces exist. Connecting a separate builder, evaluating tone and adding multilingual or persona-based output remain proposals. |
| Automated verification | xUnit exercises HTTP contracts and mapping using provider fixtures. Expand failure coverage as providers evolve. |
| Delivery | Release publishing is documented. CI is configured. Target-environment smoke checks remain open. |
| Later enhancements | Rate limits, caching and preference-aware AI require explicit quota, freshness and privacy decisions. |
