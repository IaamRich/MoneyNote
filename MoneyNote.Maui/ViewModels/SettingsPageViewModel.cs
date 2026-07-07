namespace MoneyNote.Maui;

public sealed class SettingsPageViewModel : BaseViewModel
{
    private readonly string[] _languageNames = ["English", "Russian", "Romanian"];
    private readonly string[] _languageFlags = ["en.png", "ru.png", "md.png"];
    private int _languageIndex;
    private bool _areaCash;
    private bool _areaCard;
    private bool _autoCredit;
    private bool _showSavings;
    private string _settingsHeaderSource = "settings_header.png";
    private string _todoMessage;
    private readonly LegacyLocalizationService _localizationService;

    public SettingsPageViewModel(MauiDesignDataService designDataService, LegacyLocalizationService localizationService)
    {
        _localizationService = localizationService;
        var snapshot = designDataService.GetSnapshot();
        Title = localizationService["settings_title"];
        CommonTitle = localizationService["common"];
        MoneyNotesTitle = localizationService["money_notes"];
        OtherTitle = localizationService["other"];
        ChooseLanguageLabel = localizationService["choose_language"];
        DefaultAreaLabel = localizationService["default_area"];
        ResetLabel = localizationService["reset_all"];
        ResetButtonText = localizationService["reset"];
        AllowCreditLabel = localizationService["allow_credit"];
        OverallBalanceLabel = localizationService["overall_setting"];
        CashLabel = localizationService["cash"];
        CardLabel = localizationService["card"];
        DisableAdsLabel = localizationService["dis_add"];
        DisableAdsButtonText = localizationService["pay"];
        _todoMessage = localizationService["in_developing"];

        _languageIndex = snapshot.LanguageId;
        _areaCash = snapshot.AreaCash;
        _areaCard = !snapshot.AreaCash;
        _autoCredit = snapshot.AutoCredit;
        _showSavings = snapshot.ShowSavingsInBalance;

        ToggleHeaderCommand = new Command(() =>
        {
            SettingsHeaderSource = SettingsHeaderSource == "settings_header.png" ? "settings_header_glasses.png" : "settings_header.png";
        });
        CycleLanguageCommand = new Command(() =>
        {
            _languageIndex = (_languageIndex + 1) % _languageNames.Length;
            OnPropertyChanged(nameof(LanguageName));
            OnPropertyChanged(nameof(LanguageFlag));
            TodoMessage = "TODO: apply runtime language switching after real localization services move to MAUI.";
        });
        ToggleAreaCommand = new Command(() =>
        {
            AreaCash = !AreaCash;
            AreaCard = !AreaCash;
            TodoMessage = "TODO: persist the default spending area once MAUI settings are wired.";
        });
        ToggleAutoCreditCommand = new Command(() =>
        {
            AutoCredit = !AutoCredit;
            TodoMessage = "TODO: wire auto-credit behavior to migrated finance services.";
        });
        ToggleSavingsCommand = new Command(() =>
        {
            ShowSavings = !ShowSavings;
            TodoMessage = "TODO: reconnect overall-balance preferences to the real summary calculation.";
        });
        ResetCommand = new Command(() => TodoMessage = "TODO: replace this demo reset with safe persistence-aware reset flow.");
        DisableAdsCommand = new Command(() => TodoMessage = localizationService["in_developing"]);
    }

    public string Title { get; }

    public string CommonTitle { get; }

    public string MoneyNotesTitle { get; }

    public string OtherTitle { get; }

    public string ChooseLanguageLabel { get; }

    public string DefaultAreaLabel { get; }

    public string ResetLabel { get; }

    public string ResetButtonText { get; }

    public string AllowCreditLabel { get; }

    public string OverallBalanceLabel { get; }

    public string CashLabel { get; }

    public string CardLabel { get; }

    public string DisableAdsLabel { get; }

    public string DisableAdsButtonText { get; }

    public Command ToggleHeaderCommand { get; }

    public Command CycleLanguageCommand { get; }

    public Command ToggleAreaCommand { get; }

    public Command ToggleAutoCreditCommand { get; }

    public Command ToggleSavingsCommand { get; }

    public Command ResetCommand { get; }

    public Command DisableAdsCommand { get; }

    public string SettingsHeaderSource
    {
        get => _settingsHeaderSource;
        private set => SetProperty(ref _settingsHeaderSource, value);
    }

    public string LanguageName => _languageNames[_languageIndex];

    public string LanguageFlag => _languageFlags[_languageIndex];

    public string DefaultAreaValue => AreaCash ? CashLabel : CardLabel;

    public bool AreaCash
    {
        get => _areaCash;
        private set
        {
            if (SetProperty(ref _areaCash, value))
            {
                OnPropertyChanged(nameof(DefaultAreaValue));
            }
        }
    }

    public bool AreaCard
    {
        get => _areaCard;
        private set => SetProperty(ref _areaCard, value);
    }

    public bool AutoCredit
    {
        get => _autoCredit;
        private set => SetProperty(ref _autoCredit, value);
    }

    public bool ShowSavings
    {
        get => _showSavings;
        private set => SetProperty(ref _showSavings, value);
    }

    public string TodoMessage
    {
        get => _todoMessage;
        private set => SetProperty(ref _todoMessage, value);
    }
}
