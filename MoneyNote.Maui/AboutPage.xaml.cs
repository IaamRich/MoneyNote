namespace MoneyNote.Maui;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
