namespace MoneyNote.Maui;

public partial class AccountsPage : ContentPage
{
    public AccountsPage(AccountsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
