using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed class DashboardPageViewModel : BaseViewModel
{
    private static readonly Color PositiveColor = Color.FromArgb("#738D72");
    private static readonly Color NegativeColor = Color.FromArgb("#EC7788");
    private static readonly Color NeutralColor = Color.FromArgb("#4F61A1");
    private string _deferredActionMessage;

    public DashboardPageViewModel(MauiDesignDataService designDataService, LegacyLocalizationService localizationService)
    {
        var snapshot = designDataService.GetSnapshot();
        var culture = localizationService.Culture;
        var currentBalance = snapshot.CurrentCash + snapshot.CurrentCard + (snapshot.ShowSavingsInBalance ? snapshot.CurrentSavings : 0m);

        AppName = localizationService["app_name"];
        PageTitle = localizationService["main_title"];
        BalanceLabel = localizationService["current"];
        BalanceAmount = FormatAmount(currentBalance, culture);
        NotePrompt = localizationService["enter_spent_amount"];
        NoteActionLabel = localizationService["note"];
        RecentTransactionsLabel = localizationService["last_notes"];
        StartupSliceTitle = localizationService["startup_slice_title"];
        StartupSliceBody = localizationService["startup_slice_body"];
        _deferredActionMessage = localizationService["todo_add_note"];

        BalanceCards = new ObservableCollection<BalanceCardData>
        {
            new(localizationService["current_card"], FormatAmount(snapshot.CurrentCard, culture), PositiveColor),
            new(localizationService["current_cash"], FormatAmount(snapshot.CurrentCash, culture), PositiveColor),
            new(localizationService["current_savings"], FormatAmount(snapshot.CurrentSavings, culture), PositiveColor),
            new(localizationService["current_credit"], FormatAmount(snapshot.CurrentCredit, culture), snapshot.CurrentCredit == 0m ? PositiveColor : NegativeColor)
        };

        RecentTransactions = new ObservableCollection<TransactionListItem>(
            snapshot.Transactions
                .OrderByDescending(transaction => transaction.Date)
                .Take(6)
                .Select(transaction => MapTransaction(transaction, culture)));

        AddSalaryCommand = new Command(() => DeferredActionMessage = localizationService["todo_add_note"]);
        BankCommand = new Command(() => DeferredActionMessage = "TODO: recreate the old bank transfer flow in MAUI once service migration starts.");
        SaveCommand = new Command(() => DeferredActionMessage = "TODO: recreate the old savings popup and transfer logic.");
        AddSpendCommand = new Command(() => DeferredActionMessage = localizationService["todo_add_note"]);
    }

    public string AppName { get; }

    public string PageTitle { get; }

    public string BalanceLabel { get; }

    public string BalanceAmount { get; }

    public string NotePrompt { get; }

    public string NoteActionLabel { get; }

    public string RecentTransactionsLabel { get; }

    public string StartupSliceTitle { get; }

    public string StartupSliceBody { get; }

    public ObservableCollection<BalanceCardData> BalanceCards { get; }

    public ObservableCollection<TransactionListItem> RecentTransactions { get; }

    public Command AddSalaryCommand { get; }

    public Command BankCommand { get; }

    public Command SaveCommand { get; }

    public Command AddSpendCommand { get; }

    public string DeferredActionMessage
    {
        get => _deferredActionMessage;
        private set => SetProperty(ref _deferredActionMessage, value);
    }

    private static TransactionListItem MapTransaction(Transaction transaction, CultureInfo culture)
    {
        var amountColor = transaction.MathSymbol switch
        {
            '+' => PositiveColor,
            '-' => NegativeColor,
            _ => NeutralColor
        };

        return new TransactionListItem(
            transaction.Note ?? transaction.Category?.Name ?? transaction.Type.ToString(),
            $"{transaction.Category?.Name ?? transaction.Type.ToString()} • {transaction.Date.ToString("dd.MM.yyyy", culture)}",
            $"{transaction.MathSymbol} {FormatAmount(transaction.Value, culture)}",
            amountColor,
            string.IsNullOrWhiteSpace(transaction.Category?.Image) ? "logo.png" : transaction.Category.Image,
            transaction.Bill.ToString());
    }

    private static string FormatAmount(decimal value, CultureInfo culture)
    {
        return value.ToString("F0", culture);
    }
}
