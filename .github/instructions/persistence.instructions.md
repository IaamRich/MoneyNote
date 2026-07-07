---
applyTo: "MoneyNote/Services/**/*.cs,MoneyNote/Helpers/**/*.cs,MoneyNote/Models/**/*.cs,MoneyNote/Dtos/**/*.cs"
---

- `CrossSettings` and `Helpers/Settings.cs` hold persisted finance data and settings; preserve existing keys and default-value behavior unless a task explicitly asks for a change.
- Keep `TransactionDto` serialization shape compatible with existing stored JSON.
- Be careful with destructive paths such as reset, delete, or clear operations.
- Preserve decimal money semantics and culture-sensitive date or currency behavior when formatting or parsing values.
