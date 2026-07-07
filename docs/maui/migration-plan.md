# Moneynotique / Money Note — .NET MAUI UI-First Migration Plan

_Last updated: 2026-07-07_

## Purpose

This document is the active migration plan for moving **Moneynotique / Money Note** from legacy **Xamarin.Forms** to **.NET MAUI**.

The current goal is **not** to prove every old service, setting, or persistence detail before work can continue. The current goal is to quickly turn `MoneyNote.Maui` into a MAUI-native app that visually and navigationally resembles the old Xamarin.Forms application.

Legacy services, settings, persistence, and finance calculations may be stubbed, simplified, or rewritten later. The old Xamarin.Forms code remains a visual and behavioral reference, not a structure that must be preserved internally.

## Current migration status

### Completed

- `MoneyNote.Maui` exists as a .NET MAUI single-project app.
- `MoneyNote.Maui` is included in the solution.
- The app has already been launched on Android at least once.
- The project is no longer only an incompatible legacy Xamarin.Forms solution.
- Old Xamarin.Forms, Android, and iOS source folders remain available as migration reference.

### Current limitation

`MoneyNote.Maui` is still far from the real app.

It currently contains only an initial MAUI shell / first dashboard attempt. The old app UI, navigation, popups, and screens are not yet properly recreated in MAUI.

## Current priority

The current priority is **UI-first MAUI reconstruction**.

Focus on recreating:

- main app visual structure;
- main dashboard screen;
- menu/flyout/navigation structure;
- major page skeletons;
- shared colors/styles/images;
- popup placeholders or MAUI-native modal equivalents.

Do **not** block UI migration on:

- exact `CrossSettings` / shared-preferences compatibility;
- proving old Android settings file names;
- full finance calculation parity;
- real write flows;
- exact legacy storage migration;
- full localization parity.

Those can be handled later after the MAUI UI structure exists.

## Copilot usage rule

Use small, delta-only Copilot tasks.

Do **not** ask Copilot to repeatedly run Android, emulator/device validation, broad reviews, full migration audits, or long validation loops.

Default validation expectation:

- Do not run Android unless explicitly requested.
- Do not repeatedly prove Android launch.
- Do not run broad repository validation.
- Do not perform read-only review tasks unless explicitly requested.
- Build only if needed to avoid leaving obvious compile errors, or if the prompt explicitly asks for it.

Default final report should be short:

- changed files;
- UI pieces recreated;
- stubbed/deferred functionality;
- any obvious blocker.

## Repository state

The repository currently contains:

- legacy shared Xamarin.Forms project: `MoneyNote`;
- legacy Android head: `MoneyNote.Android`;
- legacy iOS head: `MoneyNote.iOS`;
- new MAUI target app: `MoneyNote.Maui`.

The legacy Xamarin.Forms projects are not the future structure. They should mainly be used as source reference while `MoneyNote.Maui` becomes the real app.

Do not physically delete old source folders during early migration unless explicitly requested.

## Migration principles

1. **MAUI is the target app.**
   All new app work should happen in `MoneyNote.Maui` using normal MAUI project structure.

2. **UI parity comes first.**
   Recreate the old visual structure before spending time on exact old storage/service behavior.

3. **Old code is reference, not sacred.**
   The original app was written years ago and may contain messy junior-era code. Use it to understand UI and behavior, but rewrite cleanly where practical.

4. **Stub services are allowed.**
   Temporary sample/in-memory data is acceptable while building MAUI UI and navigation.

5. **Do not blindly copy Xamarin.Forms XAML.**
   Convert old XAML to MAUI-compatible XAML. Avoid unsupported Xamarin.Forms APIs and packages.

6. **Keep changes focused.**
   Each Copilot task should target one small UI/navigation slice.

7. **Avoid old package drag-in.**
   Do not add Xamarin.Forms-specific packages to `MoneyNote.Maui` just to make old code compile.

8. **Persistence can wait.**
   Real storage/write compatibility is a later task. Do not spend current UI migration effort proving legacy settings internals.

## Important old source references

Use these old Xamarin.Forms areas as visual/behavior reference:

- `MoneyNote/App.xaml`
- `MoneyNote/App.xaml.cs`
- `MoneyNote/AppBootstrapper.cs`
- `MoneyNote/Views/**`
- `MoneyNote/ViewModels/**`
- `MoneyNote/Styles/**`
- `MoneyNote/Resources/**`
- `MoneyNote/Models/**`
- `MoneyNote/Dtos/**`
- `MoneyNote/Services/**` only for understanding old labels/data shape, not as mandatory architecture

## MAUI target structure

The target app should live under `MoneyNote.Maui`.

Expected MAUI areas:

- `MoneyNote.Maui/MoneyNote.Maui.csproj`
- `MoneyNote.Maui/MauiProgram.cs`
- `MoneyNote.Maui/App.xaml`
- `MoneyNote.Maui/App.xaml.cs`
- `MoneyNote.Maui/Views/**`
- `MoneyNote.Maui/ViewModels/**`
- `MoneyNote.Maui/Services/**`
- `MoneyNote.Maui/Models/**`
- `MoneyNote.Maui/Dtos/**`
- `MoneyNote.Maui/Resources/Styles/**`
- `MoneyNote.Maui/Resources/Images/**`
- `MoneyNote.Maui/Platforms/Android/**`
- `MoneyNote.Maui/Platforms/iOS/**`

## UI-first phased plan

### Phase 1 — MAUI project bootstrap — DONE

Result:

- `MoneyNote.Maui` exists.
- The app can launch on Android.
- The app has a basic startup path.

Do not recreate the MAUI project from scratch.

### Phase 2 — Main UI/navigation reconstruction — CURRENT

Goal:

Recreate the old app's main visual/navigation structure in MAUI.

Focus:

- replace temporary/simple dashboard with a closer visual port of the old main screen;
- create MAUI-native navigation/menu structure;
- recreate shared colors, styles, spacing, fonts, and images as practical;
- add skeleton pages for major old screens if needed;
- use stub/sample/in-memory data where real services are not ready.

Allowed:

- MAUI-native pages;
- MAUI-native ViewModels;
- sample dashboard values;
- placeholder commands;
- TODOs for real services and persistence;
- modal/page placeholders instead of full old popup logic.

Deferred:

- exact legacy storage compatibility;
- full finance write flows;
- exact old popup behavior;
- complete localization system;
- charts and complex custom controls unless simple MAUI equivalents are quick.

### Phase 3 — Major screen skeletons

Create navigable MAUI skeletons for the main old app areas:

- dashboard/main;
- history;
- diagram/statistics;
- account/balance;
- settings;
- about/help/static pages.

The screens may use sample data initially. The priority is layout and navigation structure.

### Phase 4 — Popup and modal UI reconstruction

Recreate important popup flows using MAUI-native approaches:

- modal pages;
- bottom sheets if a package is deliberately chosen;
- CommunityToolkit.Maui popup only if explicitly accepted;
- simple placeholder views where old behavior is too coupled.

Do not add `Rg.Plugins.Popup`.

### Phase 5 — Service and command rewrites

After the UI is recognizable, rewrite services and commands cleanly:

- add spend flow;
- add income/add flow;
- add savings/bank/credit flows;
- add note edit/delete flows;
- add history filtering;
- add diagram aggregation;
- add settings changes.

This should be done one flow at a time.

### Phase 6 — Persistence and data compatibility

Only after the UI structure is useful, decide how to handle persistence:

- keep legacy keys if upgrade compatibility matters;
- use new clean storage if old data does not matter;
- write a deliberate migration if needed;
- avoid accidental destructive changes.

Do not spend current UI migration tasks proving `CrossSettings` internals unless explicitly requested.

### Phase 7 — Polish and parity

After UI and main commands exist:

- improve visual parity;
- finish localization;
- replace charts/cards/animations with MAUI-compatible components;
- verify Android behavior;
- add iOS support if still needed;
- clean up old Xamarin references from the solution when safe.

## Current recommended Copilot task

Use this as the current task direction, not as a broad autonomous migration request:

```text
Continue the UI-first MAUI migration.

Goal:
Make MoneyNote.Maui visually closer to the old Xamarin.Forms app by recreating the main app UI/navigation structure. Prioritize MAUI-native UI parity over legacy services/settings/persistence.

Use old MoneyNote Xamarin.Forms Views/Styles/Resources/ViewModels only as reference. Target project is MoneyNote.Maui.

Allowed paths:
- MoneyNote.Maui/**
- docs/maui/** only if notes are necessary

Do:
- recreate main screen UI closer to the old app
- add basic navigation/menu screen structure if needed
- use stub/sample/in-memory data where old services are messy
- leave TODOs for real services, commands, persistence, and finance logic

Do not:
- modify old Xamarin projects
- add Xamarin.Forms/Xamarin-specific packages
- work on CrossSettings/shared-preferences compatibility
- implement storage migration
- run Android/emulator/device validation unless explicitly asked
- commit/push/stage/reset/stash

Final report only:
changed files, recreated UI pieces, stubbed/deferred functionality.
```

## Allowed paths for current UI-first work

Default allowed paths:

- `MoneyNote.Maui/**`
- `docs/maui/**` only when documentation updates are necessary
- `MoneyNote.sln` only if strictly needed

## Forbidden by default

Do not:

- modify old `MoneyNote` Xamarin.Forms source except reading it;
- delete old Xamarin.Forms, Android, or iOS source folders;
- add `Xamarin.Forms` packages to `MoneyNote.Maui`;
- add `ReactiveUI.XamForms`;
- add `Rg.Plugins.Popup`;
- add `Xam.Plugins.Settings`;
- add `XF.Material`;
- add Xamarin-specific packages to `MoneyNote.Maui`;
- investigate `CrossSettings` / Android shared-preferences unless explicitly requested;
- implement storage migration unless explicitly requested;
- add telemetry, analytics, ads, auth, cloud sync, remote database, payment integrations, external finance APIs, or new network calls;
- run Android/emulator/device validation unless explicitly requested;
- stage, commit, push, reset, stash, change branches/remotes, or touch external systems.

## Dependency direction

Prefer MAUI-native equivalents:

| Old dependency / pattern | Direction |
| --- | --- |
| `Xamarin.Forms` | Replace with .NET MAUI controls/pages/resources |
| `ReactiveUI.XamForms` | Avoid for now unless explicitly chosen later |
| `Rg.Plugins.Popup` | Replace with MAUI modal pages/placeholders or a later deliberate popup choice |
| `Xam.Plugins.Settings` / `Plugin.Settings` | Defer; use stubs/sample data for UI-first work |
| `XF.Material` | Replace with MAUI styles/resources |
| `Xamarin.FFImageLoading.*` | Use MAUI images/resources first |
| `CardsView` / `PanCardView` | Use simple MAUI layouts or CarouselView later |
| `Microcharts.Forms` / `SkiaSharp.Forms` | Defer chart parity until diagram screen work |
| `TouchEffect` / `TouchView` | Use MAUI gestures/visual states later |
| `Microsoft.AppCenter.*` | Do not add replacement telemetry automatically |

## Screen migration order

Preferred UI-first order:

1. Main dashboard visual structure.
2. Menu/flyout/navigation structure.
3. History screen skeleton.
4. Diagram/statistics screen skeleton.
5. Account/balance screen skeleton.
6. Settings screen skeleton.
7. About/static screens.
8. Popup/modal placeholders.
9. Real commands and services.
10. Persistence/write behavior.

## What can be stubbed

The following can be fake/sample/in-memory during UI migration:

- balances;
- recent transactions;
- category totals;
- chart values;
- menu commands;
- popup commands;
- settings values;
- localization strings;
- transaction creation/edit/delete commands.

Stubbed behavior should be marked with TODOs so it can be replaced later.

## What should not be over-engineered now

Do not spend current Copilot tasks on:

- exact old `CrossSettings` storage file names;
- perfect upgrade path from old installed Xamarin app;
- full financial calculation refactor;
- complete localization parity;
- old popup behavior parity;
- iOS launch parity;
- CI/CD setup;
- automated test infrastructure;
- full architecture rewrite beyond the current UI slice.

## Manual visual checklist

Use this later for human review, not as a Copilot requirement for every prompt:

- main screen visually resembles old app;
- primary balance/card areas are recognizable;
- menu/navigation resembles old app structure;
- major old pages have MAUI destinations or placeholders;
- old icons/images are present where practical;
- colors/styles roughly match old app;
- placeholder data is clearly temporary;
- app remains understandable and ready for service rewrite.

## Notes for future data/persistence work

Persistence and finance safety still matter later, but they are not the current blocker for UI migration.

When that work starts, revisit:

- legacy keys;
- `transactiondto` shape;
- enum serialization;
- reset behavior;
- balance math;
- category totals;
- history grouping;
- diagram calculations.

Do that as a separate focused task, not mixed into UI reconstruction.
