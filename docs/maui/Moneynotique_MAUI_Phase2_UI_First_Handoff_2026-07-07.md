# MoneyNote.Maui — Phase 2B UI-First Handoff

_Last updated: 2026-07-07_

This is the active handoff for the next Copilot task.

## Current state

- `MoneyNote.Maui` exists.
- `MoneyNote.Maui` is in `MoneyNote.sln`.
- Android launch works.
- A first read-only dashboard slice exists.
- The app is still not visually/navigationally close enough to the old Xamarin.Forms app.

## Correct priority now

The next task is **UI-first MAUI reconstruction**.

Do **not** spend this task proving legacy `CrossSettings`, Android `SharedPreferences`, or storage compatibility.

Services, persistence, and finance logic can be temporary, stubbed, sample-data based, or rewritten later. The priority is to make the MAUI app look and navigate like the old app while staying buildable and Android-runnable.

## Goal for the next Copilot task

Recreate the main visible app structure in `MoneyNote.Maui`:

- startup/main page;
- main money dashboard screen;
- menu/flyout or equivalent navigation;
- history screen skeleton;
- diagram screen skeleton;
- account/settings/about screen skeletons if visible in old UI;
- popup/dialog placeholders or simple modal pages for major old popup entry points;
- shared colors/styles/resources/images needed for UI parity.

## Source reference

Use old Xamarin.Forms code as visual/behavior reference only:

- `MoneyNote/App.xaml`
- `MoneyNote/App.xaml.cs`
- `MoneyNote/AppBootstrapper.cs`
- `MoneyNote/Views/**`
- `MoneyNote/ViewModels/**`
- `MoneyNote/Styles/**`
- `MoneyNote/Resources/**`
- `MoneyNote/Models/**`
- `MoneyNote/Dtos/**`
- `MoneyNote/Services/**` only to understand labels/data shape/screen behavior

Do not modify old Xamarin source.

## Allowed changes

- `MoneyNote.Maui/**`
- `docs/maui/**` if documenting migration notes
- `MoneyNote.sln` only if strictly needed

## Forbidden changes

- do not modify old `MoneyNote` Xamarin.Forms source except reading it;
- do not delete old Xamarin.Forms, Android, or iOS source folders;
- do not add `Xamarin.Forms` packages to `MoneyNote.Maui`;
- do not add `ReactiveUI.XamForms`, `Rg.Plugins.Popup`, `Xam.Plugins.Settings`, `XF.Material`, or Xamarin-specific packages to `MoneyNote.Maui`;
- do not spend this task proving `CrossSettings` or Android shared-preference compatibility;
- do not block UI migration on legacy storage/service compatibility;
- do not implement destructive persistence migration;
- do not add telemetry, analytics, ads, auth, cloud sync, remote database, external finance APIs, payment integrations, or network calls;
- do not stage, commit, push, reset, stash, change branches/remotes, or touch external systems.

## Implementation guidance

1. Inspect old UI structure and list visible screens/navigation.
2. Create MAUI-native Views, ViewModels, resources, and navigation structure.
3. Convert old Xamarin.Forms XAML concepts to MAUI-compatible XAML instead of blindly copying unsupported syntax.
4. Use sample/in-memory data where real services are not ready.
5. Stub not-yet-migrated commands with clear TODOs.
6. Keep the app Android-runnable after every meaningful change.

## Validation

- restore `MoneyNote.Maui`;
- build `MoneyNote.Maui`;
- run/debug `MoneyNote.Maui` on Android if available;
- verify the app has real navigable screens, not only the read-only dashboard;
- verify no Xamarin.Forms-only packages were added.

## Final report expected

Copilot should report:

- changed files;
- old UI files used as reference;
- recreated MAUI screens;
- navigation structure;
- stubbed services/commands;
- deferred functionality;
- build/run result;
- Android launch result;
- risks;
- next recommended UI migration step.
