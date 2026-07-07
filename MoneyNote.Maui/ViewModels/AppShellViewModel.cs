namespace MoneyNote.Maui;

public sealed class AppShellViewModel : BaseViewModel
{
    private readonly LegacyLocalizationService _localizationService;
    private FlyoutMenuItem? _selectedMenuItem;
    private string _footerText;

    public AppShellViewModel(LegacyLocalizationService localizationService)
    {
        _localizationService = localizationService;
        AppName = localizationService["app_name"];
        VersionText = localizationService["about_version"];
        _footerText = localizationService["menu_footer"];
        MenuItems =
        [
            new FlyoutMenuItem(AppRoute.Main, localizationService["menu_main"], "main.png"),
            new FlyoutMenuItem(AppRoute.Account, localizationService["menu_account"], "account.png"),
            new FlyoutMenuItem(AppRoute.History, localizationService["menu_history"], "history.png"),
            new FlyoutMenuItem(AppRoute.Diagram, localizationService["menu_diagram"], "analitics.png"),
            new FlyoutMenuItem(AppRoute.Settings, localizationService["menu_settings"], "settings.png"),
            new FlyoutMenuItem(AppRoute.About, localizationService["menu_about"], "about.png")
        ];
    }

    public string AppName { get; }

    public string VersionText { get; }

    public IReadOnlyList<FlyoutMenuItem> MenuItems { get; }

    public FlyoutMenuItem? SelectedMenuItem
    {
        get => _selectedMenuItem;
        set => SetProperty(ref _selectedMenuItem, value);
    }

    public string FooterText
    {
        get => _footerText;
        private set => SetProperty(ref _footerText, value);
    }

    public void RunSharePlaceholder()
    {
        FooterText = $"{_localizationService["menu_footer"]} {_localizationService["in_developing"]}";
    }

    public void RunLikePlaceholder()
    {
        FooterText = $"{_localizationService["about_version"]} - {_localizationService["in_developing"]}";
    }
}
