# Moneynotique Copilot architecture summary

## Repository characteristics discovered during the audit

- Solution: `MoneyNote.sln`
- Shared app: `MoneyNote/MoneyNote.csproj` targeting `netstandard2.0`
- Mobile heads: `MoneyNote.Android` and `MoneyNote.iOS`
- UI stack: Xamarin.Forms 4.8, XAML pages and controls, popup views, shared resource dictionaries
- MVVM stack: ReactiveUI with Splat registration and routed navigation, plus popup-driven flows
- Persistence: `Plugin.Settings` / `CrossSettings` and JSON serialization in `Helpers/Settings.cs`
- Finance domain: transactions, categories, balances, savings, credit, history grouping, and diagram aggregation
- Integrations visible in code: AppCenter Analytics/Crashes, Xamarin.Essentials Share and Launcher
- Tests: none found
- CI/CD: no `.github/workflows` found
- Existing Copilot customization before this change: none found under `.github/**` and no `AGENTS.md`

## Final repository agents

| Agent | Tools | Purpose |
| --- | --- | --- |
| `Orchestrator` | `read`, `search`, `agent` | Route work to the smallest useful path and avoid automatic full-chain delegation |
| `Developer` | `read`, `search`, `edit`, `execute` | Make focused changes and use the smallest relevant validation |
| `Moneynotique Research` | `read`, `search` | Read-only investigation of Xamarin.Forms UI flow, MVVM wiring, persistence, finance logic, docs, and external behavior |
| `Reviewer` | `read`, `search` | Read-only diff and risk review focused on regressions and finance safety |

### Routing examples

1. **Simple question about a binding or file location** -> Orchestrator answers directly.
2. **Need to trace a popup, command, or history bug before editing** -> Orchestrator delegates to `Moneynotique Research`.
3. **Need to change a ViewModel, XAML page, or persistence behavior** -> Orchestrator delegates to `Developer`.
4. **Need a focused risk check after changes** -> Orchestrator delegates to `Reviewer`.

The architecture intentionally avoids an automatic Research -> Developer -> Reviewer chain. Each handoff should be explicit and justified by the task.

## Skills added

| Skill | Why it exists in this repository |
| --- | --- |
| `xamarin-forms-xaml-mvvm` | The shared app has many XAML pages, custom controls, popups, converters, and ReactiveUI/Splat bindings that often need flow tracing |
| `finance-calculation-regression-review` | Money, savings, credit, transaction totals, history grouping, and diagrams are core behavior and easy to regress |
| `persistence-impact-check` | The app stores finance data and settings in `CrossSettings` plus JSON, so storage-impact checks are repeatable and important |

## Path-specific instructions added

| File | Scope | Purpose |
| --- | --- | --- |
| `.github/instructions/xaml-ui.instructions.md` | `MoneyNote/**/*.xaml` | Protect bindings, commands, converters, resources, and popup/control contracts |
| `.github/instructions/viewmodels.instructions.md` | `MoneyNote/ViewModels/**/*.cs` | Keep ReactiveUI/Splat navigation and finance behavior safe |
| `.github/instructions/persistence.instructions.md` | `MoneyNote/Services/**/*.cs`, `MoneyNote/Helpers/**/*.cs`, `MoneyNote/Models/**/*.cs`, `MoneyNote/Dtos/**/*.cs` | Preserve persisted data keys, JSON shape, and destructive-path safety |
| `.github/instructions/platform-heads.instructions.md` | `MoneyNote.Android/**/*`, `MoneyNote.iOS/**/*` | Protect legacy mobile head configuration and renderer contracts |

## Root instruction strategy

`.github/copilot-instructions.md` keeps only universal repository facts:

- current implementation is Xamarin.Forms
- MAUI should only shape responses when a task explicitly asks for migration or roadmap planning
- shared app plus Android/iOS heads
- preserve ReactiveUI/Splat and popup navigation patterns
- protect persisted finance data
- avoid new telemetry, ads, tracking, network sync, or destructive data changes unless explicitly requested
- use the smallest relevant validation

Detailed path rules were intentionally moved out of the root file to avoid duplication and reduce always-on prompt size.

## External-system behavior notes

- The app currently initializes AppCenter Analytics and Crashes in shared and platform startup code.
- The menu uses Xamarin.Essentials `Share` and `Launcher` for external actions.
- No repository GitHub Actions workflows, repository MCP configuration, or other Copilot customization were present before this setup.
- This Copilot architecture does not add any new external integrations.

## Xamarin.Forms and MAUI notes

- Xamarin.Forms is the current implementation and remains the baseline for maintenance tasks.
- The user has indicated MAUI as future planning context, but the audited repository itself is still purely Xamarin.Forms today.
- Shared XAML, converters, popup pages, and ViewModel property names should be treated as tightly coupled until migration work is explicitly requested.
- Android and iOS heads are legacy Xamarin projects; avoid manifest, signing, icon, permission, and provisioning changes unless explicitly requested.

## Finance-calculation safety notes

- `MainViewModel`, `AccountViewModel`, `HistoryViewModel`, and `DiagramViewModel` all participate in user-visible finance behavior.
- `MoneyService`, `TransactionService`, `SettingsService`, and `Helpers/Settings.cs` affect persisted balances, transactions, and settings.
- Decimal calculations, `MathSymbol`, `TransactionType`, `TransactionBill`, and localized date formatting are high-risk surfaces.
- Reset, delete, and clear behavior should always be treated as destructive.

## Maintenance guidance

- Keep agent files focused; if a rule only applies to a path, put it in `.github/instructions/**` instead of an agent.
- Prefer skills for repeatable workflows and checklists, not for one-off repository facts.
- Do not pin models unless a future repository need clearly justifies it.
- Do not add wildcard tool permissions; list only the tools each agent needs.
- If future automation adds tests or CI, update the root instructions and this summary so `Developer` can prefer the real validation commands.
