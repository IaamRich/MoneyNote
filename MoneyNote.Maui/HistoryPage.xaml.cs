namespace MoneyNote.Maui;

public partial class HistoryPage : ContentPage
{
    public HistoryPage(HistoryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
