---
name: xamarin-forms-xaml-mvvm
description: Trace Xamarin.Forms XAML, bindings, commands, popups, and ReactiveUI or Splat MVVM wiring in Moneynotique.
---

# Xamarin.Forms XAML and MVVM tracing

Use this skill when a task touches XAML pages, controls, popups, resource dictionaries, converters, or routed page navigation.

1. Start at the XAML or code-behind file and list bindings, commands, converters, named elements, resource keys, and direct event handlers.
2. Follow the binding context to the matching ViewModel or popup ViewModel.
3. Check `ReactiveCommand`, `ICommand`, `RoutingState`, `Locator.Current`, and `PopupNavigation` usage before assuming a flow is correct.
4. Confirm shared resources, images, and converter contracts still match the XAML.
5. If a shared control changes, verify whether Android or iOS renderers depend on that contract.
6. Report the exact files and bindings or commands that were traced, plus any runtime-only assumptions that could not be proven statically.
