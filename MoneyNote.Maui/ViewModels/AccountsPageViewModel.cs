using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed class AccountsPageViewModel
{
    private static readonly Color PositiveColor = Color.FromArgb("#738D72");
    private static readonly Color NegativeColor = Color.FromArgb("#EC7788");

    public AccountsPageViewModel(MauiDesignDataService designDataService, LegacyLocalizationService localizationService)
    {
        var snapshot = designDataService.GetSnapshot();
        var culture = localizationService.Culture;
        var monthlyIncome = snapshot.Transactions.Where(item => item.Type == TransactionType.Add && SameMonth(item.Date, DateTime.Today)).Sum(item => item.Value);
        var allIncome = snapshot.Transactions.Where(item => item.Type == TransactionType.Add).Sum(item => item.Value);
        var monthlyOutlay = snapshot.Transactions.Where(item => item.Type == TransactionType.Spend && SameMonth(item.Date, DateTime.Today)).Sum(item => item.Value);
        var allOutlay = snapshot.Transactions.Where(item => item.Type == TransactionType.Spend).Sum(item => item.Value);
        var monthlySavings = snapshot.Transactions.Where(item => item.Type == TransactionType.Save && SameMonth(item.Date, DateTime.Today)).Sum(item => item.Value);

        Title = localizationService["accounts_title"];
        Sections = new ObservableCollection<AccountSection>
        {
            new(localizationService["bills"],
            [
                new AccountMetric(localizationService["my_cash"], FormatAmount(snapshot.CurrentCash, culture), PositiveColor),
                new AccountMetric(localizationService["my_card"], FormatAmount(snapshot.CurrentCard, culture), PositiveColor)
            ]),
            new(localizationService["savings"],
            [
                new AccountMetric(localizationService["month_saving"], FormatAmount(monthlySavings, culture), PositiveColor),
                new AccountMetric(localizationService["all_saving"], FormatAmount(snapshot.CurrentSavings, culture), PositiveColor)
            ]),
            new(localizationService["income_outlay"],
            [
                new AccountMetric(localizationService["month_income"], FormatAmount(monthlyIncome, culture), PositiveColor),
                new AccountMetric(localizationService["all_income"], FormatAmount(allIncome, culture), PositiveColor),
                new AccountMetric(localizationService["month_outlay"], FormatAmount(monthlyOutlay, culture), NegativeColor),
                new AccountMetric(localizationService["all_outlay"], FormatAmount(allOutlay, culture), NegativeColor)
            ]),
            new(localizationService["credits"],
            [
                new AccountMetric(localizationService["account_credit"], FormatAmount(snapshot.CurrentCredit, culture), snapshot.CurrentCredit > 0m ? NegativeColor : PositiveColor)
            ])
        };
    }

    public string Title { get; }

    public ObservableCollection<AccountSection> Sections { get; }

    private static bool SameMonth(DateTime left, DateTime right)
    {
        return left.Month == right.Month && left.Year == right.Year;
    }

    private static string FormatAmount(decimal value, CultureInfo culture)
    {
        return value.ToString("F0", culture);
    }
}
