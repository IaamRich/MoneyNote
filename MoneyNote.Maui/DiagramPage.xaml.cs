namespace MoneyNote.Maui;

public partial class DiagramPage : ContentPage
{
    public DiagramPage(DiagramPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
