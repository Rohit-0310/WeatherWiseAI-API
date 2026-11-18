# Security policy

No supported-version or patch SLA has been published. Both applications are under development.

Use the [repository Security tab](https://github.com/Rohit-0310/WeatherWiseAI-API/security) if private vulnerability reporting is enabled. Otherwise contact the repository owner privately to arrange a secure reporting channel. Keep vulnerability details out of public issues and pull requests. Include affected revisions, impact and a redacted reproduction. Never include live credentials.

Set Gemini credentials only through private server configuration. Review HTTP diagnostics because the outbound credential is sent in a query string. Weather and geocoding logs can contain coordinates and search terms. If a credential is exposed, revoke or rotate it and review relevant logs and history. The public API routes have no caller authentication or rate limiting in the current implementation.
