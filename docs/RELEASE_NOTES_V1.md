# Draft API release notes for v1.0.0

**Status: unreleased and not ready for stable release.** Source baseline `cbaddc1`. The audit changed documentation only. This draft does not establish a published version or deployment.

## Verified in source

- Current weather and European AQI, hourly and daily forecasts, location search and reverse lookup, and solar/lunar data.
- On-demand Gemini summary, clothing recommendation and activity suggestion using client-supplied weather strings.
- Open-Meteo resilience, MVC error envelopes, Development Swagger and host health endpoint. AI has a separate response shape.

## Setup and compatibility

.NET 10, outbound access to provider endpoints, and a private Gemini credential for generation. Local browser origin must be `http://localhost:3000` with API at `https://localhost:7008/api/v1`. See [deployment](DEPLOYMENT.md). No contract changes were made in this audit. Before tagging, compare against the version actually distributed and document any migration. Stabilizing error envelopes or enforcing previously inactive validation can change client behavior.

## Checks and release gates

- Release build passed on .NET SDK 10.0.402 with 0 errors and 12 repeated NU1903 warnings for two high-severity AutoMapper 16.2.0 advisories ([GHSA-6vhm-j9h7-f9gh](https://github.com/advisories/GHSA-6vhm-j9h7-f9gh), [GHSA-gpwp-5797-mjff](https://github.com/advisories/GHSA-gpwp-5797-mjff)).
- Test discovery exited successfully and found no tests. Local HTTP smoke confirmed health and Swagger UI (200), all seven business paths in Swagger JSON, development CORS preflight (204), and structured 400 responses for malformed binding and an out-of-range astronomy coordinate. Live providers, HTTPS browser integration and deployment were not exercised.
- Resolve the advisory findings, wire request validators, add tests, verify data and timezone semantics, and review error contracts, CORS, rate limiting and credential logging. Verify live providers, hosting and frontend integration before approving a stable version. [Audit](DOCUMENTATION_AUDIT.md)

The Runtime smoke test also emitted an AutoMapper license warning. Review applicable terms and resolve licensing before deployment. [AutoMapper licensing guidance](https://docs.automapper.io/en/latest/License-configuration.html)

## Documentation recheck on 2026-11-19

The Release build completed with zero errors and 12 repeated NU1903 warnings covering the two AutoMapper advisories above. Test discovery produced no test cases. The documentation checks covered local links and examples against source. Live provider requests, browser behavior and deployment were outside this recheck.
