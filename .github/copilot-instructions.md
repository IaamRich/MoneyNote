# Moneynotique Copilot instructions

- This repository is a legacy mobile personal-finance app with a shared `MoneyNote` `netstandard2.0` project plus `MoneyNote.Android` and `MoneyNote.iOS` heads.
- The current implementation is Xamarin.Forms. Preserve existing Xamarin.Forms behavior in normal maintenance tasks.
- The current implementation is not MAUI. If roadmap or migration planning is requested, treat a future move to .NET MAUI as user-provided planning context rather than an audited repository fact.
- Preserve the existing ReactiveUI, Splat, popup, and routed-navigation patterns instead of introducing a different MVVM stack during ordinary changes.
- Treat persisted finance data as user data. Do not reset, migrate, or rewrite storage formats or settings keys unless the user explicitly requests that behavior.
- Be careful with money totals, savings, credit, transaction history, and culture-specific date or currency formatting.
- Do not add new network calls, telemetry, analytics, ads, tracking, or cloud sync unless the user explicitly asks for them.
- Prefer the smallest useful change and the smallest useful validation for the task. This repository currently has no test projects and no GitHub Actions workflows.
