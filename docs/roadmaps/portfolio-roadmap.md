# Core.Extensions portfolio roadmap

## Snapshot
- 90-day evidence: 24 commits, 76 files changed
- Last signal: `f5160b2`
- Top modified areas: `Extensions` and `PolyhydraGames.Extensions.Tests`
- Stack: .NET
- Docs folder: yes
- Roadmap folder: no
- Features docs: yes
- Tests indexed: yes (`PolyhydraGames.Extensions.Tests`, `Extensions.Test1`)

## Implemented now (V1 baseline)
- Core extension utilities and behavior are documented in `docs/features/sub-module-extensions.md`, `core-capabilities.md`, and `core-application-logic.md`.
- NuGet/package hygiene is represented in `sub-module-nuget.md` and `standardized-project-layout.md`.
- A migration from legacy tests into a dedicated test project has already started, showing active quality intent.

## Gaps and opportunities
- Contract-level documentation is present but API stability checks are not yet codified.
- Packaging expectations for consumers are inferred from docs rather than tied to a validation checklist.
- Test organization still spans multiple historical test projects; ownership is unclear by extension family.

## V1 (stability)
- [ ] Add an extension API stability checklist for every exported namespace change.
- [ ] Consolidate and standardize smoke checks for all public extension families (`Async`, `String`, `Collection`, `Timer`, etc.).
- [ ] Add a compile smoke matrix for source + package restore + sample consumer project.
- [ ] Publish a release playbook for package versioning and compatibility assumptions.

## V2 (confidence)
- [ ] Formalize test ownership per feature file (for extension family and project owner).
- [ ] Add framework-target matrix for tested extension behavior.
- [ ] Add changelog discipline for behavior-impacting method changes and deprecations.
- [ ] Expand negative-path and edge-case coverage in the primary test project.

## V10 (scale)
- [ ] Establish a reusable contract template for all shared extension repos in the organization.
- [ ] Add API snapshot checks in CI to detect breaking signature changes.
- [ ] Publish compatibility matrix and deprecation windows as part of release gates.
- [ ] Reduce duplicate test patterns and centralize shared test fixtures.

## Delivery and readiness
- [ ] New extension change has a checklist reference, test evidence, and package impact note.
- [ ] Release build validates docs, tests, and restore for at least one downstream consumer.
- [ ] Roadmap entry updated when any breaking behavior is introduced.
