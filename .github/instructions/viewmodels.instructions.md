---
applyTo: "MoneyNote/ViewModels/**/*.cs"
---

- Follow the existing ReactiveUI and Splat patterns instead of introducing a new MVVM or DI framework during normal maintenance.
- Preserve routed navigation, popup flows, and property names used by XAML bindings.
- Treat money, savings, credit, balances, and history aggregation logic as behavior-critical.
- If the user asks for MAUI planning, separate planning guidance from immediate Xamarin.Forms maintenance changes.
