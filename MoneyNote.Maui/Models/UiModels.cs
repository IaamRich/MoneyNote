using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed record FlyoutMenuItem(AppRoute Route, string Title, string Icon);

public sealed record BalanceCardData(string Title, string ValueText, Color ValueColor);

public sealed record TransactionListItem(
    string Title,
    string Subtitle,
    string AmountText,
    Color AmountColor,
    string Icon,
    string BillText);

public sealed record AccountMetric(string Title, string AmountText, Color AmountColor);

public sealed record AccountSection(string Title, IReadOnlyList<AccountMetric> Metrics);

public sealed record HistoryDayGroup(string DateText, IReadOnlyList<TransactionListItem> Transactions);

public sealed record DiagramShareData(
    string Name,
    string PercentageText,
    string ValueText,
    string Icon,
    Color BarColor,
    double Ratio);

public sealed record FinanceDesignSnapshot(
    decimal CurrentCash,
    decimal CurrentCard,
    decimal CurrentSavings,
    decimal CurrentCredit,
    bool ShowSavingsInBalance,
    bool AreaCash,
    bool AutoCredit,
    int LanguageId,
    IReadOnlyList<Transaction> Transactions);
