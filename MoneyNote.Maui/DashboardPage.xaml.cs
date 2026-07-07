namespace MoneyNote.Maui;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
