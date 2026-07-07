using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed class DiagramPageViewModel : BaseViewModel
{
    private readonly LegacyLocalizationService _localizationService;
    private readonly List<Transaction> _transactions;
    private readonly IReadOnlyList<DateTime> _months;
    private bool _showIncome;
    private int _monthIndex;
    private string _summaryLabel = string.Empty;
    private string _totalText = string.Empty;

    public DiagramPageViewModel(MauiDesignDataService designDataService, LegacyLocalizationService localizationService)
    {
        _localizationService = localizationService;
        _transactions = designDataService.GetSnapshot().Transactions.ToList();
        _months = _transactions
            .Select(item => new DateTime(item.Date.Year, item.Date.Month, 1))
            .Distinct()
            .OrderBy(item => item)
            .ToList();
        _monthIndex = _months.Count - 1;

        Title = localizationService["menu_diagram"];
        Shares = [];
        GoMonthBackCommand = new Command(() =>
        {
            if (_monthIndex > 0)
            {
                _monthIndex--;
                Refresh();
            }
        });
        GoMonthForwardCommand = new Command(() =>
        {
            if (_monthIndex < _months.Count - 1)
            {
                _monthIndex++;
                Refresh();
            }
        });
        ShowOutlayCommand = new Command(() =>
        {
            _showIncome = false;
            Refresh();
        });
        ShowIncomeCommand = new Command(() =>
        {
            _showIncome = true;
            Refresh();
        });

        Refresh();
    }

    public string Title { get; }

    public ObservableCollection<DiagramShareData> Shares { get; }

    public Command GoMonthBackCommand { get; }

    public Command GoMonthForwardCommand { get; }

    public Command ShowOutlayCommand { get; }

    public Command ShowIncomeCommand { get; }

    public string CurrentMonthText => _months[_monthIndex].ToString("MMMM yyyy", _localizationService.Culture);

    public string SummaryLabel
    {
        get => _summaryLabel;
        private set => SetProperty(ref _summaryLabel, value);
    }

    public string TotalText
    {
        get => _totalText;
        private set => SetProperty(ref _totalText, value);
    }

    public bool CanGoBack => _monthIndex > 0;

    public bool CanGoForward => _monthIndex < _months.Count - 1;

    private void Refresh()
    {
        var currentMonth = _months[_monthIndex];
        var type = _showIncome ? TransactionType.Add : TransactionType.Spend;
        var monthTransactions = _transactions
            .Where(item => item.Type == type && item.Date.Month == currentMonth.Month && item.Date.Year == currentMonth.Year)
            .ToList();
        var total = monthTransactions.Sum(item => item.Value);

        SummaryLabel = _showIncome ? _localizationService["income_by_percentage"] : _localizationService["outlay_by_percentage"];
        TotalText = total.ToString("F0", _localizationService.Culture);

        Shares.Clear();
        foreach (var group in monthTransactions.GroupBy(item => item.Category?.Type ?? CategoryType.Other).OrderByDescending(group => group.Sum(item => item.Value)))
        {
            var value = group.Sum(item => item.Value);
            var ratio = total == 0m ? 0d : (double)(value / total);
            var sample = group.First();
            Shares.Add(new DiagramShareData(
                sample.Category?.Name ?? group.Key.ToString(),
                total == 0m ? "0%" : $"{ratio:P0}",
                value.ToString("F0", _localizationService.Culture),
                string.IsNullOrWhiteSpace(sample.Category?.Image) ? "other.png" : sample.Category.Image,
                ResolveColor(group.Key),
                ratio));
        }

        OnPropertyChanged(nameof(CurrentMonthText));
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(CanGoForward));
    }

    private static Color ResolveColor(CategoryType categoryType)
    {
        return categoryType switch
        {
            CategoryType.Market => Color.FromArgb("#9DAF9C"),
            CategoryType.Restaurant => Color.FromArgb("#50624F"),
            CategoryType.Transport => Color.FromArgb("#A5535F"),
            CategoryType.Business => Color.FromArgb("#F19FAB"),
            CategoryType.Network => Color.FromArgb("#989690"),
            CategoryType.Entertainment => Color.FromArgb("#7AA37A"),
            CategoryType.Salary => Color.FromArgb("#99CC99"),
            CategoryType.Gift => Color.FromArgb("#A2A29C"),
            CategoryType.Earnings => Color.FromArgb("#5E2F36"),
            CategoryType.Medicine => Color.FromArgb("#AFDDAD"),
            _ => Color.FromArgb("#C7D1C6")
        };
    }
}
