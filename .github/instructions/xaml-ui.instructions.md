---
applyTo: "MoneyNote/**/*.xaml"
---

- Preserve existing Xamarin.Forms XAML structure unless a task explicitly needs a broader rewrite.
- Be careful with bindings, command names, converter usage, resource keys, image names, and merged dictionaries.
- Keep popup layouts and custom controls aligned with their code-behind and ViewModel contracts.
- When changing a shared control or resource, check whether other pages, popups, or platform renderers depend on it.
