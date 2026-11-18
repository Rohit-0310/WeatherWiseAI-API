# Errors, logging and resilience

| Source | HTTP behavior |
| --- | --- |
| Invalid MVC model state | 400, standard envelope with string errors |
| Thrown FluentValidation exception | 400, standard envelope |
| AppException | 400 by default, or 404/401 for the mapped codes |
| UnauthorizedAccessException | 401, standard envelope |
| Other unhandled exception | 500, generic message in the standard envelope |
| OperationCanceledException or immediate inner cancellation | 499, no JSON body written by the handler |
| Gemini upstream or parsing failure caught by the adapter | 400, nested error object |
| BigDataCloud lookup failure | Normally 200 with coordinate fallback |

The middleware logs unhandled exceptions and writes camel-case JSON. Framework and proxy responses can have other shapes. See [API reference](API_GUIDELINES.md).

Serilog writes to console and a daily rolling file under `logs/`. Open-Meteo logs outbound method, full URI, status and elapsed time. Coordinates and search text can appear in logs. Restrict access and retention. Gemini uses a credential in its outbound URL query string. Verify HTTP diagnostic logging and redaction before using a real key in shared environments. [Security](../SECURITY.md)

Open-Meteo has a configured HttpClient timeout (30 seconds by default) and Polly transient HTTP handling for request exceptions, 5xx responses and 408. Retries use delays of 2, 4 and 8 seconds. The circuit breaker opens after five consecutive handled failures and waits 30 seconds before probing. These policies do not cover Gemini or BigDataCloud. There is no stale weather cache or AI fallback. An air-quality request failure can fail the current-weather call.
