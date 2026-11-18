# Coding standards

Use nullable-aware C# and the existing feature folders. Use PascalCase for public members, camelCase for locals, an underscore for private fields and an I prefix for interfaces. Name payloads by role (Request, Response or DTO). Keep controllers focused on binding, delegation and HTTP responses. Define provider interfaces in Application and map upstream payloads before exposing them.

Use async I/O and propagate cancellation where supported. Prefer explicit validation with tests for binding and business rules. Preserve existing error envelopes and units for compatible changes. Use structured logging and omit credentials and sensitive provider payloads. Apply the [project rules](../PROJECT_RULES.md) and update [API reference](API_GUIDELINES.md) for contract changes.
