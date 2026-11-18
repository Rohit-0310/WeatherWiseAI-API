# WeatherWiseAI API project rules

- Keep HTTP routing, middleware and composition in API. Keep feature services, DTOs and provider interfaces in Application. Implement provider HTTP calls in Infrastructure. Keep Shared and Domain independent.
- Keep the current solution stateless. Propose persistence or new dependencies explicitly before introducing them.
- Use Refit for the established Open-Meteo integration, typed HttpClient where already used, FluentValidation for feature rules and Serilog for structured logging. Verify that validation actually runs when adding rules.
- Keep credentials in private runtime configuration. Avoid logging secrets, request credentials or private provider payloads.
- Preserve API compatibility. Document parameters, units, defaults and error shapes. Coordinate frontend changes when a contract changes.
- Keep provider calls asynchronous and propagate cancellation where available. Keep prompt logic out of controllers. The existing AI adapter builds an inline prompt. Moving it behind Application prompt abstractions remains planned.
- Follow [coding standards](docs/CODING_STANDARDS.md) and [contribution checks](CONTRIBUTING.md). Use Conventional Commits. Update docs and tests when behavior changes.
