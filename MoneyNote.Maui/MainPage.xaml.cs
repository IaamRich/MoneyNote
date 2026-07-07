using Microsoft.Extensions.DependencyInjection;

namespace MoneyNote.Maui;

public partial class MainPage : FlyoutPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppShellViewModel _viewModel;

    public MainPage(
        AppShellViewModel viewModel,
        IServiceProvider serviceProvider,
        NavigationCoordinator navigationCoordinator)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _serviceProvider = serviceProvider;
        BindingContext = viewModel;

        navigationCoordinator.NavigationRequested += route => NavigateTo(route, updateSelection: true);

        NavigateTo(viewModel.MenuItems[0].Route, updateSelection: false);
        _viewModel.SelectedMenuItem = _viewModel.MenuItems[0];
    }

    private void OnMenuSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FlyoutMenuItem menuItem)
        {
            NavigateTo(menuItem.Route, updateSelection: false);
        }
    }

    private void OnShareClicked(object? sender, EventArgs e)
    {
        _viewModel.RunSharePlaceholder();
    }

    private void OnLikeClicked(object? sender, EventArgs e)
    {
        _viewModel.RunLikePlaceholder();
    }

    private void NavigateTo(AppRoute route, bool updateSelection)
    {
        ContentPage page = route switch
        {
            AppRoute.Main => _serviceProvider.GetRequiredService<DashboardPage>(),
            AppRoute.Account => _serviceProvider.GetRequiredService<AccountsPage>(),
            AppRoute.History => _serviceProvider.GetRequiredService<HistoryPage>(),
            AppRoute.Diagram => _serviceProvider.GetRequiredService<DiagramPage>(),
            AppRoute.Settings => _serviceProvider.GetRequiredService<SettingsPage>(),
            AppRoute.About => _serviceProvider.GetRequiredService<AboutPage>(),
            _ => _serviceProvider.GetRequiredService<DashboardPage>()
        };

        Detail = new NavigationPage(page)
        {
            BarBackgroundColor = Color.FromArgb("#738d72"),
            BarTextColor = Colors.White
        };

        if (updateSelection)
        {
            _viewModel.SelectedMenuItem = _viewModel.MenuItems.FirstOrDefault(item => item.Route == route);
        }

        IsPresented = false;
    }
}
