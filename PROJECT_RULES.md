# WeatherWiseAI API project rules

- Keep HTTP routing, middleware and composition in API. Keep feature services, DTOs and provider interfaces in Application. Implement provider HTTP calls in Infrastructure. Keep Shared and Domain independent.
- Keep the current solution stateless. Propose persistence or new dependencies explicitly before introducing them.
- Use Refit for the established Open-Meteo integration, typed HttpClient where already used, FluentValidation for feature rules and Serilog for structured logging. Verify that validation actually runs when adding rules.
- Keep credentials in private runtime configuration. Avoid logging secrets, request credentials or private provider payloads.
- Preserve API compatibility. Document parameters, units, defaults and error shapes. Coordinate frontend changes when a contract changes.
- Keep provider calls asynchronous and propagate cancellation where available. Keep prompt logic out of controllers. The existing AI adapter builds an inline prompt. Moving it behind Application prompt abstractions remains planned.
- Follow [coding standards](docs/CODING_STANDARDS.md) and [contribution checks](CONTRIBUTING.md). Use Conventional Commits. Update docs and tests when behavior changes.

## Reviewing an API change

| Concern | Contributor standard | Example to inspect |
| --- | --- | --- |
| Controllers and dependency direction | Bind input, delegate through an Application interface, and translate the result into the route's documented HTTP response. Register concrete adapters at composition time. | WeatherController and AiSummaryController |
| DTO ownership | Keep provider field names and transport types inside the adapter. Give client-facing fields explicit names and preserve their units and nullability. | HourlyMappingProfile and HourlyResponseDto |
| Validation | Put reusable rules next to their requests. Exercise invalid input through HTTP to establish whether the rule runs. | HourlyRequestValidator and the astronomy request's range attributes |
| Configuration and logging | Bind provider configuration in Infrastructure. Use structured fields for diagnostics and keep credentials out of URLs in shared logs and examples. | OpenMeteoOptions and GeminiOptions |
| Async and DI | Inject dependencies in constructors, await outbound I/O, and pass cancellation tokens through supported calls. Use the feature registration extensions for service lifetimes. | HourlyService and AddApplicationServices |
| Error compatibility | Verify status and body for binding errors, exceptions and provider failure results separately. | [Error handling](docs/ERROR_HANDLING.md) |
| Testability | Isolate external I/O behind interfaces so service behavior can be checked using deterministic provider fixtures. | IWeatherProvider |

## Submitting work

Keep changes focused on one use case. For a new route, update the contract and README endpoint list. For a new dependency or integration boundary, update architecture and decisions. Move a roadmap item to implemented once its behavior is available through the application. Use a feature branch and a descriptive Conventional Commit. See [contributing](CONTRIBUTING.md) for checks.
