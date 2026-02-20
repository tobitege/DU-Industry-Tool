# Branching and Build Lanes

This repository intentionally uses a low-maintenance two-lane model.

## Branches

1. `master` (legacy lane)

- Primary maintenance line.
- Targets .NET Framework (`net48`) and Visual Studio 2019-era compatibility.
- Existing solutions remain valid:
  - `DU-Industry-Tool.sln`
  - `DU-Industry-Tool.KryptonSource.sln` (local development flow)

1. `next-2026` (next lane)

- Forward-looking line for Visual Studio 2026+.
- Uses solution XML format (`.slnx`) for next-generation tooling.
- Solution file:
  - `DU-Industry-Tool.Next.slnx`

## Explicit non-goals

- No dedicated VS2022-only branch/lane.
- No third long-lived maintenance line.

## CI scope

- CI/release automation is for NuGet/release workflow concerns and is separate from local KryptonSource development workflow.
- Local day-to-day work with `DU-Industry-Tool.KryptonSource.sln` is not dictated by CI matrix decisions.

## Merge policy

1. Default direction: `master` -> `next-2026`.
2. Backport from `next-2026` to `master` only when explicitly needed (critical fixes).
