using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace MoneyNote.Maui;

public sealed class HistoryPageViewModel : BaseViewModel
{
    private static readonly Color PositiveColor = Color.FromArgb("#738D72");
    private static readonly Color NegativeColor = Color.FromArgb("#EC7788");
    private static readonly Color NeutralColor = Color.FromArgb("#4F61A1");
    private readonly LegacyLocalizationService _localizationService;
    private readonly NavigationCoordinator _navigationCoordinator;
    private readonly List<Transaction> _transactions;
    private string _searchText = string.Empty;
    private string _filterLabel;
    private bool _isSearchPanelVisible;
    private int _filterIndex;

    public HistoryPageViewModel(
        MauiDesignDataService designDataService,
        LegacyLocalizationService localizationService,
        NavigationCoordinator navigationCoordinator)
    {
        _localizationService = localizationService;
        _navigationCoordinator = navigationCoordinator;
        _transactions = designDataService.GetSnapshot().Transactions.OrderByDescending(item => item.Date).ToList();
        _filterLabel = localizationService["history_filter_all"];

        Title = localizationService["menu_history"];
        SearchHint = localizationService["history_search_hint"];
        Groups = [];

        SearchCommand = new Command(() => IsSearchPanelVisible = !IsSearchPanelVisible);
        ChangeFilterCommand = new Command(CycleFilter);
        DiagramCommand = new Command(() => _navigationCoordinator.NavigateTo(AppRoute.Diagram));

        RefreshGroups();
    }

    public string Title { get; }

    public string SearchHint { get; }

    public ObservableCollection<HistoryDayGroup> Groups { get; }

    public Command SearchCommand { get; }

    public Command ChangeFilterCommand { get; }

    public Command DiagramCommand { get; }

    public bool IsSearchPanelVisible
    {
        get => _isSearchPanelVisible;
        set => SetProperty(ref _isSearchPanelVisible, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                RefreshGroups();
            }
        }
    }

    public string FilterLabel
    {
        get => _filterLabel;
        private set => SetProperty(ref _filterLabel, value);
    }

    private void CycleFilter()
    {
        _filterIndex = (_filterIndex + 1) % 3;
        FilterLabel = _filterIndex switch
        {
            1 => _localizationService["history_filter_week"],
            2 => _localizationService["history_filter_month"],
            _ => _localizationService["history_filter_all"]
        };

        RefreshGroups();
    }

    private void RefreshGroups()
    {
        var query = _transactions.AsEnumerable();

        if (_filterIndex == 1)
        {
            query = query.Where(item => item.Date >= DateTime.Today.AddDays(-7));
        }
        else if (_filterIndex == 2)
        {
            query = query.Where(item => item.Date >= DateTime.Today.AddDays(-31));
        }

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            query = query.Where(item =>
                (item.Note?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                || item.Value.ToString(CultureInfo.InvariantCulture).Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        Groups.Clear();
        foreach (var group in query.GroupBy(item => item.Date.Date))
        {
            Groups.Add(new HistoryDayGroup(
                group.Key.ToString("dddd, dd MMMM", _localizationService.Culture),
                group.Select(item => new TransactionListItem(
                    item.Note ?? item.Category?.Name ?? item.Type.ToString(),
                    $"{item.Bill} • {item.Date:HH:mm}",
                    $"{item.MathSymbol} {item.Value.ToString("F0", _localizationService.Culture)}",
                    item.MathSymbol switch
                    {
                        '+' => PositiveColor,
                        '-' => NegativeColor,
                        _ => NeutralColor
                    },
                    string.IsNullOrWhiteSpace(item.Category?.Image) ? "logo.png" : item.Category.Image,
                    item.Bill.ToString())).ToList()));
        }
    }
}
