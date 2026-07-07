---
applyTo: "MoneyNote.Android/**/*,MoneyNote.iOS/**/*"
---

- Android and iOS projects are legacy Xamarin heads for the shared `MoneyNote` app.
- Do not change manifests, signing, bundle IDs, provisioning, app icons, launch assets, or platform permissions unless the user explicitly requests it.
- Keep custom renderer contracts aligned with the shared controls they extend.
- Treat platform initialization code such as Forms, popup, image, material, and analytics startup as behavior-sensitive.
