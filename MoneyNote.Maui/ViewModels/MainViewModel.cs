using System.Globalization;
using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed class MainViewModel
{
    private static readonly Color PositiveColor = Color.FromArgb("#738D72");
    private static readonly Color NegativeColor = Color.FromArgb("#EC7788");
    private static readonly Color NeutralColor = Color.FromArgb("#4F61A1");
    private readonly LegacyLocalizationService _localizationService;

    public MainViewModel(LegacyDashboardService dashboardService, LegacyLocalizationService localizationService)
    {
        _localizationService = localizationService;
        var snapshot = dashboardService.GetSnapshot();

        AppName = localizationService["app_name"];
        PageTitle = localizationService["main_title"];
        BalanceLabel = localizationService["current"];
        BalanceAmount = FormatAmount(snapshot.CurrentBalance);
        StartupSliceTitle = localizationService["startup_slice_title"];
        StartupSliceBody = localizationService["startup_slice_body"];
        RecentTransactionsLabel = localizationService["last_notes"];
        EmptyTransactionsTitle = localizationService["last_notes"];
        EmptyTransactionsBody = localizationService["alert_no_history"];
        BalanceCards =
        [
            new BalanceCard(localizationService["current_cash"], FormatAmount(snapshot.CurrentCash), PositiveColor),
            new BalanceCard(localizationService["current_card"], FormatAmount(snapshot.CurrentCard), PositiveColor),
            new BalanceCard(localizationService["current_savings"], FormatAmount(snapshot.CurrentSavings), PositiveColor),
            new BalanceCard(
                localizationService["current_credit"],
                FormatAmount(snapshot.CurrentCredit),
                snapshot.CurrentCredit == 0m ? PositiveColor : NegativeColor)
        ];
        LastTransactions = snapshot.RecentTransactions
            .Select(CreateTransactionItem)
            .ToList();
    }

    public string AppName { get; }

    public string PageTitle { get; }

    public string BalanceLabel { get; }

    public string BalanceAmount { get; }

    public string StartupSliceTitle { get; }

    public string StartupSliceBody { get; }

    public string RecentTransactionsLabel { get; }

    public string EmptyTransactionsTitle { get; }

    public string EmptyTransactionsBody { get; }

    public IReadOnlyList<BalanceCard> BalanceCards { get; }

    public IReadOnlyList<TransactionItem> LastTransactions { get; }

    private TransactionItem CreateTransactionItem(Transaction transaction)
    {
        var transactionLabel = string.IsNullOrWhiteSpace(transaction.Note)
            ? transaction.Category?.Name ?? transaction.Type.ToString()
            : transaction.Note;
        var subtitleParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(transaction.Category?.Name))
        {
            subtitleParts.Add(transaction.Category.Name);
        }

        if (transaction.Date != default)
        {
            subtitleParts.Add(transaction.Date.ToString("d", _localizationService.Culture));
        }

        return new TransactionItem(
            transactionLabel,
            string.Join(" • ", subtitleParts),
            $"{transaction.MathSymbol} {FormatAmount(transaction.Value)}",
            transaction.MathSymbol switch
            {
                '+' => PositiveColor,
                '-' => NegativeColor,
                _ => NeutralColor
            },
            string.IsNullOrWhiteSpace(transaction.Category?.Image) ? "logo.png" : transaction.Category.Image);
    }

    private string FormatAmount(decimal value)
    {
        return value.ToString("F0", _localizationService.Culture);
    }

    public sealed record BalanceCard(string Title, string ValueText, Color ValueColor);

    public sealed record TransactionItem(string Title, string Subtitle, string AmountText, Color AmountColor, string Icon);
}
