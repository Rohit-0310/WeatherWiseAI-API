# Assistant guidance

Use [project rules](PROJECT_RULES.md), [coding standards](docs/CODING_STANDARDS.md) and the [current architecture](docs/ARCHITECTURE.md). Inspect the feature's route, DTO, service and registrations before editing or documenting behavior. Preserve the stateless provider architecture and existing contracts unless a change is requested. Use repository-relative examples and keep credentials and private configuration out of output.

## Working on a feature

1. Find the controller action and read its request, result, and client-visible error paths. Use the layered [project map](docs/ARCHITECTURE.md) to find the implementation behind each injected interface.
2. Keep HTTP binding in Api, the use case in Application and provider-specific transport and JSON in Infrastructure. Reuse the existing condition mapper when exposing weather codes.
3. Check the feature's registration and lifetime. Preserve cancellation and mapping behavior when changing a provider call.
4. Review the corresponding Web service before changing fields, units, defaults or errors. Record observable changes in the API reference and run the checks in the development guide.
