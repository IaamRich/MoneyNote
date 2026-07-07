namespace MoneyNote.Maui;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider, LegacyLocalizationService localizationService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        localizationService.ApplyCulture();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var mainPage = _serviceProvider.GetRequiredService<MainPage>();
        return new Window(mainPage);
    }
}
