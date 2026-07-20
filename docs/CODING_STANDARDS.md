# Coding Standards

To maintain high code quality in WeatherWiseAI-API, all contributors and AI assistants must adhere to the following standards.

## Naming Conventions
- **Classes/Interfaces/Structs**: `PascalCase`
- **Methods**: `PascalCase`
- **Method Parameters**: `camelCase`
- **Local Variables**: `camelCase`
- **Private Fields**: `_camelCase`
- **Constants**: `PascalCase`
- **Interfaces**: Must start with `I` (e.g., `IWeatherService`).

## Folder Conventions
- Use plural names for folders containing specific types (e.g., `Controllers`, `Services`, `Interfaces`, `DTOs`).
- Group files by feature if a layer becomes too large, but default to technical grouping for simplicity.

## Dependency Rules
- **Strictly adhere to Clean Architecture**. Inner layers must never reference outer layers.
- `WeatherWiseAI.Domain` has **zero** external dependencies.
- Use Dependency Injection via `IServiceCollection` for all services. Never use `new` for services.

## SOLID
- **Single Responsibility**: A class should have one reason to change. (e.g., a Controller routes; a Service processes; a Validator validates).
- **Open/Closed**: Code should be open for extension, closed for modification.
- **Liskov Substitution**: Subtypes must be substitutable for their base types.
- **Interface Segregation**: Prefer many small, client-specific interfaces over one large interface.
- **Dependency Inversion**: Depend on abstractions, not concretions.

## Clean Code
- Methods should do one thing and be short.
- Avoid deep nesting; return early.
- Avoid magic strings/numbers; move them to the `Shared/Constants` folder.

## Comments
- **XML Documentation**: Required for all public API endpoints and DTOs to generate Swagger documentation.
- **Inline Comments**: Use sparingly to explain *why* something is done, not *what* is being done. The code should explain the *what*.

## Async Guidelines
- Always use `async`/`await` for I/O operations (HTTP calls).
- Suffix asynchronous method names with `Async` (e.g., `GetWeatherAsync`).
- Pass `CancellationToken` through the entire asynchronous call stack.
- Never use `.Wait()` or `.Result` on a Task, as it can cause deadlocks.

## Exception Handling
- **Do not throw generic `Exception`**. Use specific domain exceptions (e.g., `DomainValidationException`).
- **Do not catch generic `Exception`** unless logging it at the top-level middleware.
- Allow exceptions to bubble up to the global error handling middleware.
- Only catch exceptions if you intend to recover from them or wrap them in a more meaningful domain exception.
