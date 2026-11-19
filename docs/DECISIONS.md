# Architecture decisions

| Decision in source | Consequence |
| --- | --- |
| Keep weather and location data upstream | No database or migrations. Availability and freshness depend on providers |
| Separate HTTP, application logic and provider adapters | DTOs and provider interfaces contain integration changes. The Domain project is reserved |
| Use Open-Meteo through Refit and Polly | Typed calls and transient failure handling for weather, AQI and search |
| Use BigDataCloud for reverse geocoding | Direct HTTP adapter supplies a coordinate fallback. Timezone is a GMT placeholder |
| Use Gemini through a typed HttpClient | Server manages credentials. Generated text depends on supplied inputs, model availability and quotas |
| Use AutoMapper and a shared condition mapper | Provider DTOs and condition codes are translated before reaching callers |
| Define FluentValidation rules in Application | Rules are registered. Wiring them into MVC requests remains open |

For a new decision, document context, chosen approach, alternatives and trade-offs. Keep planned changes separate from the current [architecture](ARCHITECTURE.md).

## What the boundaries buy a contributor

The weather panel consumes named hourly fields while Open-Meteo supplies parallel series. Keeping that translation in mapping profiles lets a provider field change be reviewed at the integration boundary. Changing a public field still requires checking both repositories. Refit keeps forecast query parameters visible in an interface, while its configured HttpClient supplies handlers and resilience.

A stateless host avoids database provisioning and migrations. A successful health check therefore says little about upstream availability. Gemini has its own credential, transport and error translation, so Open-Meteo's retry behavior cannot be inferred for AI generation. The validation classes make bounds discoverable to maintainers, but the HTTP pipeline must call them to turn those bounds into a client guarantee.
