---
name: finance-calculation-regression-review
description: Review Moneynotique transaction, balance, savings, credit, history, and diagram changes for finance regressions.
---

# Finance calculation regression review

Use this skill when a task changes transaction creation or deletion, balances, savings, credit handling, history grouping, search, or diagram totals.

1. Trace the affected flow through `MainViewModel`, `AccountViewModel`, `HistoryViewModel`, `DiagramViewModel`, and the money or transaction services that participate.
2. Verify that `TransactionType`, `TransactionBill`, `MathSymbol`, category selection, and persisted amounts stay aligned.
3. Check both current-month and all-time totals when the code computes both.
4. Check debit and credit edge cases separately for cash, card, savings, and bank flows.
5. Review localized parsing or formatting risks for decimal amounts and displayed dates.
6. Call out missing automated coverage or environment limits instead of assuming the flow is safe.
