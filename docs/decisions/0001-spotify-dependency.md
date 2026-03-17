---
title: "ADR: SpotifyAPI.Web Dependency Decision"
date: 2026-03-13
status: accepted
---

# ADR: SpotifyAPI.Web Dependency Decision

## Context
The `AsyncExtensions.cs` file in Core.Extensions references SpotifyAPI.Web types (`APITooManyRequestsException`, etc.) but the package was not declared as a dependency, causing build failures.

## Decision
**Option A: Add SpotifyAPI.Web PackageReference (Selected)**

We chose to add the `SpotifyAPI.Web` package (v7.0.0) directly as a dependency. This is the simplest and fastest approach.

### Why Option A?
- **Quickest to implement**: No code restructuring required
- **Low risk**: The functionality is already in use; adding the dependency restores intended behavior
- **No breaking changes**: Existing consumers continue to work (they just need the new dependency)

### Alternatives Considered
- **Option B** (Move to separate project): Overkill for a single file using generic retry patterns
- **Option C** (Conditional compilation): Adds complexity without clear benefit

## Consequences
- Added `SpotifyAPI.Web` v7.0.0 to `Extensions/Extensions.csproj`
- Updated `AsyncExtensions.cs` to use reflection-based exception handling (compatible with v7.0 API changes)
- All target frameworks (net8.0, net9.0) build successfully

## Notes
- The code originally used v6.x exception types which changed in v7.0
- Reflection is used to detect exception types for forward compatibility