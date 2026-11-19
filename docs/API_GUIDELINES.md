# API reference

Local base URL `https://localhost:7008/api/v1`. Controllers use fixed route prefixes. There is no authentication requirement, pagination protocol, or public rate-limit policy in the application. The separate `GET /health` endpoint checks host health and has no provider probes.

| Method and route | Query or body | Success data |
| --- | --- | --- |
| GET `/Weather/current` | `latitude`, `longitude`, optional `units`, `timezone`, `language` | Coordinates, conditions, temperatures, wind, humidity, pressure, visibility, day flag, AQI, UV and precipitation chance |
| GET `/Weather/hourly` | Same weather parameters, plus `hours` (default 24) | Coordinates, units, timezone, `hours[]` with time, temperature, wind speed, precipitation chance and condition |
| GET `/Weather/forecast` | Same weather parameters | Coordinates, units, timezone, `days[]` with date, temperature extremes, max wind, precipitation chance and condition. Provider default determines daily count |
| GET `/Weather/astronomy` | `latitude`, `longitude`, optional `date` (default UTC current date/time) | Date, coordinates, sunrise, sunset, solar noon, daylight and sunshine durations, lunar phase, illumination and nullable rise/set times |
| GET `/Location/search` | `query`, `count` (default 10), `language` (default en) | List of locations, possibly empty |
| GET `/Location/reverse` | `latitude`, `longitude`, `language` (default en) | Location with a coordinate fallback if lookup fails. Reverse results use GMT as a placeholder timezone |
| POST `/AiSummary` | JSON with six string fields shown below | Plain `summary`, `clothing`, `activity` object |

Send latitude from -90 to 90 and longitude from -180 to 180. The weather DTOs default to `Metric` (°C, km/h), with `Imperial` (°F, mph) available. Visibility remains in metres. Current UV and precipitation chance map from daily maximum values, and AQI is European. Weather requests fall back to provider timezone `auto`. The weather DTOs accept `language`, but the services do not use it to localize descriptions. Conditions have `code`, `description` and `providerCode`. Location fields include `id`, `name`, `latitude`, `longitude`, `country`, `region` and `timezone`.

```sh
curl "https://localhost:7008/api/v1/Weather/current?latitude=28.61&longitude=77.21&units=Metric"
```

GET response shape is `{"data": ..., "success": true, "errors": [], "meta": null}`. AI request example:

```json
{
  "location": "New Delhi",
  "temperature": "25 °C",
  "condition": "Clear sky",
  "humidity": "50%",
  "wind": "12 km/h",
  "uvIndex": "4"
}
```

AI provider failures return HTTP 400 with `{"error":{"message":"...","code":null,"details":[]}}`. Model-binding and handled exception errors use the GET-style envelope with `success: false`. See [error handling](ERROR_HANDLING.md).

FluentValidation rules describe hourly bounds of 1–48, search count 1–100 and minimum query length 2. The API registers the validators in DI but currently neither enables MVC auto-validation nor calls them in these request paths. Clients should send values within the stated bounds. Astronomy has DataAnnotations range checks. Non-nullable numeric DTO properties can default to zero when omitted. Swagger lists route schemas in Development.

## Try the two response formats

For a one-hour forecast, request `GET /api/v1/Weather/hourly?latitude=28.61&longitude=77.21&hours=1&units=Metric`. Read `data.timezone` and `data.units` alongside `data.hours[0]`. Each hour includes time, temperature, windSpeed, precipitationChance and condition. An empty location search is a successful list response and should be handled independently of HTTP failures.

Submit the six-field JSON example above to `POST /api/v1/AiSummary` using the Development Swagger UI. A synthetic example of its success body is:

```json
{"summary":"Rain is likely.","clothing":"Bring a raincoat.","activity":"Choose an indoor activity."}
```

The route prefix fixes the public version at v1. A change to wrapping, units, field names or enforcement of previously inactive validation needs a compatibility review with the browser client. Search count and forecast hours limit individual results. There is no cursor or next-page contract.
