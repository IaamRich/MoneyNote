---
name: persistence-impact-check
description: Inspect changes that affect CrossSettings, JSON transaction storage, reset behavior, or other persisted user finance data.
---

# Persistence impact check

Use this skill when a task touches `MoneyService`, `SettingsService`, `TransactionService`, `Helpers/Settings.cs`, DTOs, or models tied to stored data.

1. Inventory every persisted key or serialized object that the change can affect.
2. Separate transient UI state keys from durable finance data.
3. Preserve existing CrossSettings keys and the `TransactionDto` JSON shape unless the user explicitly requests a migration.
4. Check reset, delete, and default-value paths for destructive behavior.
5. Do not introduce silent data loss, cloud sync, telemetry, or schema rewrites.
6. Report which keys, DTOs, and call sites were inspected.
