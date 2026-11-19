# Coding standards

Use nullable-aware C# and the existing feature folders. Use PascalCase for public members, camelCase for locals, an underscore for private fields and an I prefix for interfaces. Name payloads by role (Request, Response or DTO). Keep controllers focused on binding, delegation and HTTP responses. Define provider interfaces in Application and map upstream payloads before exposing them.

Use async I/O and propagate cancellation where supported. Prefer explicit validation with tests for binding and business rules. Preserve existing error envelopes and units for compatible changes. Use structured logging and omit credentials and sensitive provider payloads. Apply the [project rules](../PROJECT_RULES.md) and update [API reference](API_GUIDELINES.md) for contract changes.

## Conventions in practice

Use names that distinguish boundaries. IHourlyService names the contract, HourlyService implements the use case, HourlyRequestValidator owns reusable input rules and IOpenMeteoClient describes upstream HTTP calls. Name asynchronous I/O methods with the Async suffix. Avoid blocking on Task.Result or Wait().

Group related request, response, mapping, service and validator files under the feature. Keep each mapping testable with plain DTOs. Prefer a small interface at the consumer boundary and constructor injection so a fixture can replace an external provider. Explain conversions or provider quirks in comments. Use XML summaries for public API semantics, especially units, defaults and nullable fields.

Catch an exception locally when the adapter has a defined recovery or translation. Otherwise allow the existing middleware to apply its policy. A change from an exception to a failure Result can change the status and JSON a caller receives.
