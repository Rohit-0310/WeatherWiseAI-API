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
