namespace MoneyNote.Maui;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsPageViewModel _viewModel;

    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    private void OnAutoCreditToggled(object? sender, ToggledEventArgs e)
    {
        _viewModel.ToggleAutoCreditCommand.Execute(null);
    }

    private void OnShowSavingsToggled(object? sender, ToggledEventArgs e)
    {
        _viewModel.ToggleSavingsCommand.Execute(null);
    }
}
