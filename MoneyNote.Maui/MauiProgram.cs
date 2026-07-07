using Microsoft.Extensions.Logging;

namespace MoneyNote.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddSingleton<LegacyLocalizationService>();
        builder.Services.AddSingleton<MauiDesignDataService>();
        builder.Services.AddSingleton<NavigationCoordinator>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddTransient<DashboardPageViewModel>();
        builder.Services.AddTransient<AccountsPageViewModel>();
        builder.Services.AddTransient<HistoryPageViewModel>();
        builder.Services.AddTransient<DiagramPageViewModel>();
        builder.Services.AddTransient<SettingsPageViewModel>();
        builder.Services.AddTransient<AboutPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<AccountsPage>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<DiagramPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<AboutPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
