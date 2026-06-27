# Code Health

Last reviewed: 2026-06-27

## Native Validation

```bash
dotnet test Extensions.sln --configuration Release --verbosity minimal
dotnet list Extensions.sln package --outdated
devstudio validate --repo /home/lancer1977/code/Core.Extensions
```

## Current Findings

- Tests pass locally: 74 passed.
- Dependency audit is clean against the configured NuGet sources.
- CI now targets the current `PolyhydraGames.Extensions.Tests` project.
- The local deleted `artifacts/PolyhydraGames.Extensions.1.0.0.nupkg` is generated package state, not source.

## Follow-Up Backlog

- Define the supported extension API surface by category and add high-risk regression examples.
- Decide whether this repo should adopt central package management with the rest of the Core family.
- Add sample snippets for common downstream consumers.
