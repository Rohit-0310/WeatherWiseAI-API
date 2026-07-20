# API Guidelines

This document outlines the standards for designing and exposing endpoints in WeatherWiseAI-API.

## REST Conventions
- URLs should use nouns, not verbs (e.g., `/api/weather`, not `/api/getWeather`).
- Use proper HTTP methods:
  - `GET` for retrieving data.
  - `POST` for creating data or invoking complex operations (like AI generation that requires large payloads).

## Response Format
All API responses should follow a standardized wrapper to ensure consistent client parsing.

```json
{
  "data": { ... },       // The actual payload
  "success": true,       // Boolean indicating success
  "errors": [],          // Array of error messages if success is false
  "meta": { ... }        // Optional metadata (pagination, trace IDs)
}
```

## Status Codes
- `200 OK`: Successful GET or POST (if not creating a resource).
- `400 Bad Request`: Validation errors or malformed requests.
- `401 Unauthorized`: Missing or invalid authentication.
- `404 Not Found`: Resource not found (e.g., invalid coordinates).
- `429 Too Many Requests`: Rate limit exceeded.
- `500 Internal Server Error`: Unhandled exceptions.
- `502 Bad Gateway`: Failure from an external service (Open-Meteo or Gemini).

## DTO Naming
- Request payloads must end with `Request` (e.g., `WeatherInsightRequest`).
- Response payloads must end with `Response` (e.g., `WeatherInsightResponse`).

## Versioning Strategy
- Versioning is done via the URL path: `/api/v1/...`
- Breaking changes require a new version (e.g., `/api/v2/...`).
- Non-breaking changes (adding fields) can be done in the current version.

## Validation
- All incoming requests must be validated using FluentValidation.
- Validation occurs before the controller action executes.
- Validation failures immediately return a `400 Bad Request` with structured error messages.

## Pagination
*(For future use, if list endpoints are added)*
- Use query parameters: `?page=1&pageSize=10`
- Include pagination details in the `meta` object of the response.

## Error Responses
Error responses must clearly state the issue without leaking stack traces.

```json
{
  "success": false,
  "errors": [
    "Latitude must be between -90 and 90.",
    "Longitude is required."
  ]
}
```
