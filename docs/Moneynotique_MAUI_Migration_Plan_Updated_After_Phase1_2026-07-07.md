# Moneynotique / Money Note — .NET MAUI Migration Plan

_Last updated: 2026-07-07_

## Purpose

This document is the working migration plan for moving **Moneynotique / Money Note** from legacy **Xamarin.Forms** to **.NET MAUI**.

The migration goal is not only to create an empty MAUI shell. The target is a normal **MAUI-standard single-project app** that gradually reaches feature parity with the old Xamarin.Forms app.

The existing Xamarin.Forms code remains the behavior and data-compatibility reference until the MAUI app reaches parity.

## Current MAUI migration status

### Completed

Phase 1 is completed.

A new `MoneyNote.Maui` .NET MAUI single-project app exists, is included in `MoneyNote.sln`, builds, and has been launched on a real Android device.

Known created/changed areas from Phase 1:

- `MoneyNote.Maui/**`
- `MoneyNote.sln`
- old Xamarin source remains available as migration reference

### Current limitation

`MoneyNote.Maui` is currently only an Android-runnable placeholder shell.

It does **not** yet contain the real migrated MoneyNote functionality from the Xamarin.Forms app.

The next implementation work must replace the placeholder with the first real migrated vertical slice, while keeping the MAUI app buildable and Android-runnable.

## Current repository state

The current repository is a legacy Xamarin.Forms solution plus a new MAUI migration project:

- legacy shared app project: `MoneyNote`
- legacy Android head: `MoneyNote.Android`
- legacy iOS head: `MoneyNote.iOS`
- new MAUI target app: `MoneyNote.Maui`
- legacy UI: Xamarin.Forms XAML pages, controls, resources, popups, converters
- legacy MVVM/navigation: ReactiveUI, Splat registration, routed navigation, popup-driven flows
- legacy persistence: `Plugin.Settings` / `CrossSettings` with JSON serialization in settings helpers
- local finance data: transactions, balances, categories, savings, credit, settings
- tests/CI: no reliable automated test or CI workflow was identified during the audit

The legacy Android/iOS Xamarin heads may appear incompatible in the current Visual Studio environment. Do not spend migration effort trying to restore legacy Xamarin tooling unless needed only for source inspection.

The target structure is the new MAUI single-project app under `MoneyNote.Maui`.

## Migration principles

1. **MAUI is the target structure.**
   Use a standard .NET MAUI single-project layout with `Platforms/Android` and `Platforms/iOS` inside `MoneyNote.Maui`.

2. **The old Xamarin projects are the reference baseline, not the future structure.**
   They may be unloaded or removed from the solution later if the MAUI replacement builds and launches, but old source folders should not be physically deleted during early migration.

3. **Android runnable has already been achieved.**
   Preserve this. Every next migration slice must keep `MoneyNote.Maui` buildable and Android-runnable.

4. **Do not migrate everything at once.**
   Migrate features vertically in small slices: startup/main screen first, then money flows, then history, diagrams, account/settings, and remaining UI.

5. **Local financial data compatibility is critical.**
   Persisted keys, JSON shape, enum serialization, default values, reset behavior, and balance calculations must be preserved intentionally.

6. **No silent redesign.**
   Current Xamarin behavior is the compatibility baseline unless a later task explicitly requests behavior changes.

7. **Do not blindly copy Xamarin.Forms XAML.**
   Old XAML may depend on Xamarin.Forms-only controls, custom renderers, effects, popup plugins, `ReactiveContentPage`, `XF.Material`, CardsView, Microcharts.Forms, FFImageLoading, or other packages that are not MAUI-compatible as-is.

## Reusable code map

### Mostly reusable as-is

- `MoneyNote/Models/Transaction.cs`
- `MoneyNote/Models/Categories.cs`
- `MoneyNote/Models/CategoryDto.cs`
- `MoneyNote/Dtos/TransactionDto.cs`

These should be inspected before reuse. They are good candidates for copying or moving into `MoneyNote.Maui` once compile compatibility is confirmed.

### Reusable after abstraction or cleanup

- `MoneyNote/Services/TransactionService.cs`
- `MoneyNote/Services/MoneyService.cs`
- `MoneyNote/Services/SettingsService.cs`
- `MoneyNote/Helpers/Settings.cs`
- `MoneyNote/Services/Contracts/*.cs`

These contain useful persistence and business behavior, but they should not be blindly copied if they depend on Xamarin.Forms, `CrossSettings`, global static settings, navigation, popups, or UI classes.

### Finance behavior to preserve

- spend/add/save/bank balance math
- current cash/card/credit/savings calculations
- transaction category mapping
- history grouping and search/filter behavior
- monthly and category percentage aggregation in diagrams
- localized category/date/amount formatting behavior

Known high-risk source areas:

- `MainViewModel`
- `AccountViewModel`
- `HistoryViewModel`
- `DiagramViewModel`
- `MoneyService`
- `TransactionService`
- `SettingsService`
- `Helpers/Settings.cs`

### ViewModel reuse approach

Do not port ViewModels wholesale at first.

Reusable parts are mostly the command bodies and business rules from:

- `MainViewModel`
- `HistoryViewModel`
- `DiagramViewModel`
- `AccountViewModel`
- `SettingsViewModel`

These currently mix business logic with UI, popup, navigation, and framework-specific behavior. Extract reusable logic into MAUI-ready services/use-cases gradually.

## Rewrite-required map

### UI layer

Rewrite or carefully port:

- `MoneyNote/Views/**/*.xaml`
- most view code-behind files
- `ReactiveContentPage` usage
- `ReactiveMasterDetailPage` usage
- popup page wiring
- shared resource dictionaries and styles
- custom controls where they depend on Xamarin.Forms APIs

### Platform startup

Legacy startup files are not the MAUI target structure:

- `MoneyNote.Android/MainActivity.cs`
- `MoneyNote.Android/SplashActivity.cs`
- Android manifests/resources in the old head
- `MoneyNote.iOS/AppDelegate.cs`
- `MoneyNote.iOS/Main.cs`
- old iOS plist/startup files

The MAUI replacement should use:

- `MoneyNote.Maui/MauiProgram.cs`
- `MoneyNote.Maui/App.xaml`
- `MoneyNote.Maui/App.xaml.cs`
- `MoneyNote.Maui/Platforms/Android/**`
- `MoneyNote.Maui/Platforms/iOS/**`

### Popups/navigation

Legacy pieces likely needing replacement/remapping:

- `Views/Popups/*`
- `Rg.Plugins.Popup`
- `MasterView`
- `RoutedViewHost`
- ReactiveUI Xamarin.Forms routing host pattern

Possible MAUI targets:

- MAUI Shell navigation
- regular MAUI navigation
- `CommunityToolkit.Maui` popup support
- ReactiveUI.Maui only if it is intentionally chosen and compatible

### Renderers/effects/custom control plumbing

Likely rewrite/replacement areas:

- `MoneyNote.Android/Renderers/*`
- `MoneyNote.iOS/Renderer/CustomEntryRenderer.cs`
- `TouchEffect` usage in XAML
- image tint/caching behavior
- card/pan/coverflow controls
- chart controls
- Lottie animation integration

## Local-data compatibility contract

Before migrating real persisted-data behavior, document and preserve the exact legacy persistence behavior.

### Critical persisted payload

- settings key: `transactiondto`
- payload shape: `TransactionDto { DataBase: List<Transaction> }`

A future MAUI storage adapter must read existing payloads without data loss.

### Enum serialization risk

The legacy app sets `JsonConvert.DefaultSettings` with `StringEnumConverter { CamelCaseText = true }`.

Persisted enum values may exist as camel-case strings such as:

- `save`
- `spend`
- `cash`
- other enum values used by transaction/bill/type models

The MAUI compatibility layer should accept both string and numeric enum representations unless source inspection proves only one format exists.

### Preference key compatibility risk

Known behavior-driving keys include, but may not be limited to:

- `CurrentCash`
- `CurrentCard`
- `CurrentCredit`
- `AllSavings`
- `CurrentLanguage`
- `IdSpendFromWhere`
- popup temporary keys
- transaction storage key(s)

Future migration work must inspect the source and list all keys/defaults exactly before changing persistence code.

### Reset semantics risk

Legacy reset behavior uses `CrossSettings.Current.Clear()`.

The MAUI app must preserve the user-visible reset behavior, but should implement it intentionally. Do not accidentally clear unrelated MAUI preferences without documenting the behavior.

### Corrupt JSON behavior

Legacy parsing appears tolerant: parse failures are swallowed and default values are returned.

The MAUI app should preserve this tolerance and should use a safe backup/restore strategy before first write to existing local data.

### Locale/language mapping risk

Language IDs are int-based and must remain canonical.

Known meaningful IDs from the audit:

- `0`
- `1`
- `2`

Do not remap language IDs casually.

## Dependency replacement table

| Current dependency | Current usage | MAUI target direction |
| --- | --- | --- |
| `Xamarin.Forms` | app/UI/platform framework | .NET MAUI single-project app |
| `Xamarin.Essentials` | share/launcher/platform APIs | built-in `Microsoft.Maui.Essentials` APIs |
| `ReactiveUI.XamForms` | reactive pages/routing host | `ReactiveUI.Maui` or MAUI MVVM / `CommunityToolkit.Mvvm` after deliberate choice |
| `Rg.Plugins.Popup` | popup pages/navigation | `CommunityToolkit.Maui` popup or custom MAUI popup flow |
| `Xam.Plugins.Settings` / `Plugin.Settings` | key-value persistence | MAUI `Preferences` behind a compatibility wrapper preserving legacy keys |
| `Newtonsoft.Json` | DTO serialization | keep `Newtonsoft.Json` during compatibility phase, or use strict converters only after parity proof |
| `XF.Material` | Material buttons/styles | MAUI handlers/resources/toolkit controls/styles |
| `Xamarin.FFImageLoading.*` | cached/tinted images | MAUI `Image`, behaviors/effects, or MAUI-compatible image caching library |
| `CardsView` / `PanCardView` | coverflow/card UI | MAUI `CarouselView` plus transforms, or compatible MAUI package |
| `Com.Airbnb.Xamarin.Forms.Lottie` | animations | MAUI-compatible Lottie package if animations are preserved |
| `Microcharts.Forms` | diagram chart view | `Microcharts.Maui` or another MAUI charting option |
| `SkiaSharp.Views.Forms` | chart/rendering surface | `SkiaSharp.Views.Maui.Controls` |
| `TouchEffect` / `TouchView` | touch visual effects | MAUI gestures/behaviors/toolkit touch behavior |
| `Microsoft.AppCenter.*` | analytics/crashes | do not add replacement telemetry unless explicitly requested |

## Revised phased migration plan

### Phase 0 — Compatibility contract documentation

Create or maintain docs that record the exact legacy behavior for:

- persisted keys and defaults
- `transactiondto` JSON shape
- enum serialization
- reset/clear behavior
- finance calculation entry points
- manual parity checks

This phase is documentation-only. It may happen before or after the MAUI skeleton, but it must be completed before migrating real persisted data writes.

### Phase 1 — First MAUI-standard Android-runnable app — DONE

Result:

- `MoneyNote.Maui` exists.
- `MoneyNote.Maui` is included in `MoneyNote.sln`.
- The MAUI app builds.
- Android launch works on a real device.
- The app currently shows placeholder UI only.
- Old Xamarin source remains the behavior reference.

Phase 1 must now be treated as complete. Do not recreate the MAUI project from scratch.

### Phase 2 — First real migrated vertical slice

Replace the placeholder MAUI page with the first real migrated app screen.

Target:

- keep `MoneyNote.Maui` buildable and Android-runnable;
- use old `MoneyNote` Xamarin.Forms code as read-only source reference;
- port reusable models/DTOs/services only as needed for the first screen;
- port or rewrite the first startup/main screen using MAUI-compatible XAML and MAUI APIs;
- do not blindly copy Xamarin.Forms XAML if it depends on unsupported controls, renderers, effects, popup plugins, or Xamarin-specific packages.

Preferred first slice:

1. startup flow;
2. main screen shell/layout;
3. minimal ViewModel/service wiring needed to show real app state;
4. no destructive persistence migration yet.

This phase should make the MAUI app visibly closer to the old app, not just another placeholder.

### Phase 3 — Compatibility storage abstraction

Introduce a MAUI-ready storage abstraction that can read the old settings keys and JSON shape unchanged.

Requirements:

- exact key compatibility
- exact default behavior or documented intentional differences
- read-before-write safety
- backup/restore strategy before first migration write
- tolerant enum deserialization
- tolerant corrupt JSON handling

Do not perform destructive storage migration. Do not clear or rewrite user data casually.

### Phase 4 — Extract reusable domain and finance logic

Move finance/business rules away from ViewModel/UI code into pure services or use-cases.

Scope:

- transaction creation/update rules
- spend/add/save/bank balance math
- category mapping
- history grouping/filtering
- diagram/monthly aggregation

This must be done in small slices with before/after parity checks.

### Phase 5 — Migrate one vertical feature at a time

Suggested order:

1. Main spend/add flow
2. bank/save flow
3. history view/search/filter/grouping
4. diagram/category totals
5. account/settings
6. about/help/static screens
7. popups and remaining UI polish

Each migrated slice should include:

- minimal UI port
- ViewModel/service wiring
- data compatibility check
- manual parity checklist
- regression notes

### Phase 6 — Replace platform-specific UI pieces

Replace legacy renderers/effects/custom controls with MAUI equivalents:

- custom entry renderer
- touch effects
- image tint/caching
- cards/coverflow
- charts
- Lottie/animations
- popup behavior

### Phase 7 — Parity hardening and release prep

Before treating the MAUI app as replacement-ready:

- verify existing local data opens without loss
- verify transaction list roundtrip
- verify balances
- verify category totals
- verify diagram percentages
- verify language switching
- verify reset behavior
- verify Android lifecycle basics
- verify iOS startup later, if iOS is still a target

## Completed implementation milestone

### Phase 1 result

The first MAUI-standard runnable version of Moneynotique has been created.

This milestone is successful because the new MAUI project can be selected and launched on Android from Visual Studio / local tooling.

### Expected Phase 1 repository shape

- `MoneyNote.Maui/MoneyNote.Maui.csproj`
- `MoneyNote.Maui/MauiProgram.cs`
- `MoneyNote.Maui/App.xaml`
- `MoneyNote.Maui/App.xaml.cs`
- `MoneyNote.Maui/MainPage.xaml`
- `MoneyNote.Maui/MainPage.xaml.cs`
- `MoneyNote.Maui/Platforms/Android/**`
- `MoneyNote.Maui/Platforms/iOS/**`
- `MoneyNote.Maui/Resources/**`
- `MoneyNote.sln` includes `MoneyNote.Maui`

The placeholder UI is temporary and must be replaced during Phase 2.

## Next implementation step

The next implementation step is to replace the placeholder MAUI page with the first real migrated vertical slice.

Recommended Copilot task:

- continue from the existing runnable `MoneyNote.Maui` project;
- do not recreate the MAUI project;
- inspect the old Xamarin.Forms app as source reference;
- port only what is needed for the first startup/main screen;
- keep the new app buildable and Android-runnable;
- defer risky screens/dependencies with explicit TODOs instead of breaking the build.

### Allowed changes for the next step

- `MoneyNote.Maui/**`
- `docs/maui/**` if documenting migration notes
- `MoneyNote.sln` only if strictly needed

### Forbidden changes for the next step

- do not modify old `MoneyNote` Xamarin.Forms source except reading it
- do not delete old Xamarin.Forms, Android, or iOS source folders
- do not add `Xamarin.Forms` packages to `MoneyNote.Maui`
- do not add `Rg.Plugins.Popup`, `ReactiveUI.XamForms`, `Xam.Plugins.Settings`, `XF.Material`, or Xamarin-specific packages to `MoneyNote.Maui`
- do not change persisted keys
- do not change JSON shape
- do not change reset behavior
- do not change local financial data semantics
- do not add telemetry, analytics, ads, auth, cloud sync, remote database, payment integrations, external finance APIs, or new network calls
- do not make release signing/provisioning/store changes
- do not stage, commit, push, reset, stash, change branches/remotes, or touch external systems

### Validation for the next step

Minimum validation:

- restore `MoneyNote.Maui`
- build `MoneyNote.Maui`
- run/debug `MoneyNote.Maui` on Android emulator or connected Android device if available
- verify the app no longer shows only the placeholder page
- verify the first migrated screen opens without crashing

If launch is not possible, report the exact blocker:

- missing MAUI workload
- missing Android SDK
- missing emulator/device
- unsupported Visual Studio version
- project target mismatch
- build error with exact file/error

### Final report required from Copilot for the next step

The final report should include:

- changed files
- old source files used as reference
- migrated functionality
- deferred functionality
- restore/build result
- Android launch result
- exact launch instructions
- risks and unknowns
- next safest migration slice

## Risks and unknowns

Known risks/unknowns:

- legacy Android/iOS projects may be incompatible with current Visual Studio/Xamarin tooling
- no tests were found during audit
- no CI workflows were found during audit
- root README is minimal/empty
- possible orphan/legacy artifact: `Views/MasterDetail.csproj`
- unknown MAUI parity for current UI libraries, animations, cards, charts, touch effects, image tinting
- persistence behavior must be verified from source before writing MAUI storage code
- AppCenter replacement should not be added automatically
- `MoneyNote.Maui` currently lacks real migrated app functionality

## Manual parity checklist

Use this checklist during feature migration and before release readiness:

- existing local data opens without loss
- transactions list is preserved
- balances are preserved
- settings are preserved
- spend/add/save/bank math matches old app for same inputs
- transaction signs and bill/source behavior match old app
- category mapping and labels match old app
- history search/filter/grouping matches old app
- diagram percentages and category totals match old app
- language switching works with the same IDs/defaults
- localized date/amount/category behavior matches old app
- popup confirm/cancel behavior matches old app
- reset behavior matches expected user-visible result
- app launches after force-close/reopen
- app handles corrupt/missing transaction JSON according to legacy behavior
